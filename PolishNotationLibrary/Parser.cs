using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenLibrary;

namespace PolishNotationLibrary
{
    /// <summary>
    /// Анализатор выражения
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Введенное выражение для синтаксического анализа
        /// </summary>
        string _expression;

        /// <summary>
        /// Индекс считываемого символа исходного выражения
        /// </summary>
        int _expressionIndex;

        /// <summary>
        /// Список лексем выражения
        /// </summary>
        List<Token> _tokenList;

        /// <summary>
        /// Выдача списка лексем выражения
        /// </summary>
        public List<Token> GetTokenList
        {
            get
            {
                return this._tokenList;
            }
        }

        /// <summary>
        /// Счетчик переменных выражения
        /// </summary>
        int _varCount;

        /// <summary>
        /// Выбранная переменная
        /// </summary>
        Variable _selectedVariable;

        /// <summary>
        /// Счетчик скобок
        /// </summary>
        int _braсketsCount;

        /// <summary>
        /// Конструктор обработчика выражения
        /// </summary>
        /// <param name="inputExpression"> Введенное пользователем выражение </param>
        public Parser(string inputExpression)
        {
            this._expression = inputExpression;
            this._expressionIndex = 0;
            this._tokenList = new List<Token>();
            this._varCount = 0;
            this._varCount = 0;
            ExpressionAnalisys();
        }

        /// <summary>
        /// Анализ введенного пользователем выражения
        /// </summary>
        void ExpressionAnalisys()
        {
            if (_expression.Length == 0)
            {
                throw new SyntaxException("Нет уравнения для решения\r\nПожалуйста, введите уравнение и повторите попытку ");
            }

            while (_expressionIndex < _expression.Length)
            {
                Token nextToken = GetToken();
                if (nextToken is End)
                {
                    break;
                } 
                if (nextToken is Bracket)
                {
                    AddBracked(nextToken);
                    continue;
                }
                if (nextToken is Operation)
                {
                    AddOperation(nextToken);
                    continue;
                }
                if (nextToken is Function)
                {
                    AddFunction(nextToken);
                }
                if (nextToken is Variable)
                {
                    AddVariable(nextToken);
                    continue;
                }
                if (nextToken is Number)
                {
                    AddNumber(nextToken);
                    continue;
                }
            }

            if (_varCount == 0)
            {
                throw new SyntaxException("В веденном уравнении нет переменной\r\nПожалуйста, исправьте ошибку и повторите попытку");
            }

            if (_braсketsCount != 0)
            {
                throw new SyntaxException("Дисбаланс скобок в веденном уравнении\r\nПожалуйста, исправьте ошибку и повторите попытку");
            }

            Token lastToken = _tokenList.Last();
            if (lastToken is Operation || (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
            {
                throw new SyntaxException("Синтаксическая ошибка", _expressionIndex, _expressionIndex);
            }
        }


        /// <summary>
        /// Получение очередной лексемы
        /// </summary>
        /// <returns> Лексема </returns>
        Token GetToken()
        {
            while (_expressionIndex < _expression.Length && Char.IsWhiteSpace(_expression[_expressionIndex]))
            {
                _expressionIndex++;
            }

            if (_expressionIndex == _expression.Length)
            {
                return new End();
            }

            if (IsBracked(_expression[_expressionIndex]))
            {
                return new Bracket(_expression[_expressionIndex++]);
            }

            if (IsOperation(_expression[_expressionIndex]))
            {
                return new Operation(_expression[_expressionIndex++]);
            }

            string temp = String.Empty;
            int tempIndex = _expressionIndex;
            while(_expressionIndex < _expression.Length && Char.IsLetter(_expression[_expressionIndex]))
            {
                temp += _expression[_expressionIndex];
                _expressionIndex++;
            }
            if (IsFunction(temp, tempIndex))
            {
                return new Function(temp);
            }
            else
            {
                _expressionIndex = tempIndex;
            }

            if (IsVariable(_expression[_expressionIndex]))
            {
                return new Variable(_expression[_expressionIndex++]);
            }

            tempIndex = _expressionIndex;
            temp = String.Empty;
            while (_expressionIndex < _expression.Length && ( Char.IsNumber(_expression[_expressionIndex]) || 
                _expression[_expressionIndex] == '.' || _expression[_expressionIndex] == ','))
            {
                temp += _expression[_expressionIndex++];
            }
            if (IsNumber(temp, tempIndex) && temp.Length != 0)
            {
                return new Number(temp);
            }
            else
            {
                _expressionIndex = tempIndex;
            }

            throw new SyntaxException("Синтаксическая ошибка!", _expressionIndex, _expressionIndex);
        }

        /// <summary>
        /// Метод определяет, является ли считанный символ скобкой
        /// </summary>
        /// <param name="ch"> Считанный символ </param>
        /// <returns> Скобка - истина, другой симол - ложь </returns>
        bool IsBracked(char ch)
        {
            return Bracket.avaliableBrackets.Contains(ch);
        }

        /// <summary>
        /// Метод определяет, является ли символ доступной операцией
        /// </summary>
        /// <param name="ch"> Считанный симовл </param>
        /// <returns> Операция - истина, другой символ - ложь </returns>
        bool IsOperation(char ch)
        {
            return Operation.avaliableOperations.Contains(ch);
        }

        /// <summary>
        /// Метод определяет, является ли строка лексемой функции
        /// </summary>
        /// <param name="str"> Считанная строка </param>
        /// <param name="tempIndex"> Индекс начала считанной строки </param>
        /// <returns>  Функция - истина, неизвестная строка - ложь </returns>
        bool IsFunction(string str, int tempIndex)
        {
            str = str.ToUpper();
            if (Function.avaliableFunctions.Contains(str))
            {
                if (_expressionIndex < _expression.Length)
                {
                    if (_expressionIndex + 1 < _expression.Length && _expression[_expressionIndex] == '(')
                    {
                        return true;
                    }
                    else
                    {
                        throw new SyntaxException(String.Format("У функции {0} нет аргумента\r\nАргумент функции необходимо вводить в круглых скобках\r\nПожалуйста, исправьте ошибку и повторите попытку",
                            str), tempIndex, _expressionIndex - 1);
                    }
                }
                else
                {
                    throw new SyntaxException(String.Format("У функции {0} нет аргумента\r\nАргумент функции необходимо вводить в круглых скобках\r\nПожалуйста, исправьте ошибку и повторите попытку",
                        str), tempIndex, _expressionIndex - 1);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод определяет, является ли лексемой переменной
        /// </summary>
        /// <param name="ch"> Считанный символ </param>
        /// <returns> Переменная - истина, другой символ - ложь </returns>
        bool IsVariable(char ch)
        {
            ch = char.ToUpper(ch);
            if (ch >= 'A' && ch <= 'Z')
            {
                if (_varCount == 0)
                {
                    _varCount++;
                    _selectedVariable = new Variable(ch);
                    return true;
                }
                else
                {
                    if (ch != Variable.GetVariableSymbol)
                    {
                        string temp = String.Empty;
                        int tempIndex = this._expressionIndex;
                        while (tempIndex < this._expression.Length && this._expression[tempIndex] >= 'a' && this._expression[tempIndex] <= 'z')
                        {
                            temp += this._expression[tempIndex++];
                        }
                        if (temp.Length == 1)
                        {
                            throw new SyntaxException(String.Format("Неизвестный символ: {0}\r\nВ уравнеии может быть только 1 переменная\r\nПожалуйста, исправьте ошибку и повторите попытку", temp), 
                                _expressionIndex, _expressionIndex);
                        }
                        else
                        {
                            throw new SyntaxException(String.Format("Неизвестная последовательность символов: {0}\r\nПожалуйста, исправьте ошибку и повторите попытку", temp),
                                _expressionIndex, tempIndex);
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Метод определяет, является ли считанная строка числом
        /// </summary>
        /// <param name="str"> Считанная строка цифр и разделителей </param>
        /// <param name="startPosition"> Позиция начала считанной строки цифр и разделителей </param>
        /// <returns> Корректно записанное вещественное число - истина, иначе - ложь </returns>
        bool IsNumber(string str, int startPosition)
        {
            double num;
            if (double.TryParse(str, out num))
            {
                return true;
            }
            if (str.Length != 0)
            {
                throw new SyntaxException(String.Format("Ошибка в записи числа {0}\r\nПо региональным настройкам Вашей операционной системы необходимо использовать в качестве разделителя символ '{1}'\r\nПожалуйста, исправьте ошибку и повторите попытку",
                    str, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator), startPosition, _expressionIndex);
            }
            return false;
        }

        /// <summary>
        /// Добавить скобку в список лексем
        /// </summary>
        /// <param name="bracketToken"> Считанная скобка </param>
        void AddBracked(Token bracketToken)
        {
            Bracket bracket = (Bracket)bracketToken;
            if (bracket.GetBracketType == Bracket.BracketType.OpenBracket)
            {
                ++_braсketsCount;
                Token lastToken;
                if (_tokenList.Count != 0)
                {
                    lastToken = _tokenList.Last();
                }
                else
                {
                    lastToken = null;
                }

                if (lastToken == null || lastToken is Operation || lastToken is Function ||
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
                {
                    _tokenList.Add(bracket);
                    return;
                }
                
                if (lastToken is Number || lastToken is Variable || 
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
                {
                    _tokenList.Add(new Operation('*'));
                    _tokenList.Add(bracket);
                    return;
                }
            }

            if (bracket.GetBracketType == Bracket.BracketType.CloseBracket)
            {
                --_braсketsCount;
                if (_braсketsCount < 0)
                {
                    throw new SyntaxException("Ошибка в комбинации скобок\r\nПожалуйста, исправьте ошибку и повторите попытку", _expressionIndex, _expressionIndex);
                }

                Token lastToken = _tokenList.Last();
                if (lastToken is Number || lastToken is Variable || 
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
                {
                    _tokenList.Add(bracket);
                    return;
                }

                if (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket)
                {
                    throw new SyntaxException("Пустое выражение в скобках!\r\nПожалуйста, исправьте ошибку и повторите попытку", (_expression.Substring(0, _expressionIndex)).LastIndexOf('('), _expressionIndex);
                }

                if (lastToken is Operation)
                {
                    throw new SyntaxException(String.Format("После знака операции {0} ожидалось число, переменная, функция или выражение в скобках\r\nПожалуйста, исправьте ошибку и повторите попытку", lastToken.GetTokenString), _expressionIndex, _expressionIndex);
                }
            }
        }

        /// <summary>
        /// Метод добавляет считанную операцию в список лексем
        /// </summary>
        /// <param name="nextOperation"> Считанная операция </param>
        void AddOperation(Token nextOperation)
        {
            Operation operation = (Operation)nextOperation;
            Token lastToken;
            if (_tokenList.Count != 0)
            {
                lastToken = _tokenList.Last();
            }
            else
            {
                lastToken = null;
            }

            if ("+-".Contains(operation.GetOperationSymbol))
            {
                if (lastToken == null || (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
                {
                    _tokenList.Add(new Number("0"));
                    _tokenList.Add(operation);
                    return;
                }

                if (lastToken is Number || lastToken is Variable ||
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
                {
                    _tokenList.Add(operation);
                    return;
                }

                if (lastToken is Operation)
                {
                    throw new SyntaxException(String.Format("Между операторами {0} и {1} нет операнда\r\nПожалуйста, исправьте ошибку и повторите попытку",
                        ((Operation)lastToken).GetOperationSymbol, operation.GetOperationSymbol), (_expression.Substring(0, _expressionIndex - 1)).LastIndexOfAny(Operation.avaliableOperations.ToCharArray()), _expressionIndex);
                }

                if (lastToken is Function)
                {
                    throw new SyntaxException("Нет агрумента функции\r\nАргумент функции следует записывать в круглые скобки\r\nПожалуйста, исправьте ошибку и повторите попытку", 
                        _expressionIndex, _expressionIndex);
                }
            }

            if ("*/^".Contains(operation.GetOperationSymbol))
            {
                if (lastToken is Number || lastToken is Variable || 
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
                {
                    _tokenList.Add(operation);
                    return;
                }

                if (lastToken == null || lastToken is Operation || lastToken is Function || 
                    (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
                {
                    string lastTokenStr = String.Empty;
                    if (lastToken != null)
                    {
                        lastTokenStr = lastToken.GetTokenString;
                    }
                    throw new SyntaxException(String.Format("Ошибка: {0}{1}\r\nПожалуйста, исправте ошибку и повторите попытку", 
                        lastTokenStr, operation.GetTokenString), _expressionIndex, _expressionIndex);
                }
            }
        }

        /// <summary>
        /// Добавить считанную функцию в список лексем
        /// </summary>
        /// <param name="nextFunction"> Считанная функция </param>
        void AddFunction(Token nextFunction)
        {
            Function function = (Function)nextFunction;
            Token lastToken;
            if (_tokenList.Count != 0)
            {
                lastToken = _tokenList.Last();
            }
            else
            {
                lastToken = null;
            }

            if (lastToken == null || lastToken is Operation ||
                (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
            {
                _tokenList.Add(function);
                return;
            }

            if (lastToken is Number || lastToken is Variable || 
                (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
            {
                _tokenList.Add(new Operation('*'));
                _tokenList.Add(function);
                return;
            }
        }

        /// <summary>
        /// Метод добавляет считанную переменную в список лексем
        /// </summary>
        /// <param name="varToken"> Считанная переменная </param>
        void AddVariable(Token varToken)
        {
            Variable variable = (Variable)varToken;
            Token lastToken;
            if (_tokenList.Count != 0)
            {
                lastToken = _tokenList.Last();
            }
            else
            {
                lastToken = null;
            }

            if (lastToken == null || lastToken is Operation || 
                (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
            {
                _tokenList.Add(variable);
                return;
            }

            if (_tokenList.Last() is Number || _tokenList.Last() is Variable ||
                (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
            {
                _tokenList.Add(new Operation('*'));
                _tokenList.Add(variable);
                return;
            }

            string lastTokenStr = String.Empty;
            if (lastToken != null)
            {
                lastTokenStr = lastToken.GetTokenString;
            }
            throw new SyntaxException(String.Format("Ошибка: {0}{1}\r\nПожалуйста, исправте ошибку и повторите попытку", lastTokenStr, variable.GetTokenString), _expressionIndex, _expressionIndex);
        }

        /// <summary>
        /// Добавить числоа в список лексем
        /// </summary>
        /// <param name="nextNumber"> Считанное число </param>
        void AddNumber(Token nextNumber)
        {
            Number number = (Number)nextNumber;
            Token lastToken;
            if (_tokenList.Count != 0)
            {
                lastToken = _tokenList.Last();
            }
            else
            {
                lastToken = null;
            }

            if (lastToken == null || lastToken is Operation ||
                (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.OpenBracket))
            {
                _tokenList.Add(number);
                return;
            }

            if (lastToken is Variable || (lastToken is Bracket && ((Bracket)lastToken).GetBracketType == Bracket.BracketType.CloseBracket))
            {
                _tokenList.Add(new Operation('*'));
                _tokenList.Add(number);
                return;
            }

            if (lastToken is Number)
            {
                int startPosition = _expressionIndex - 1;
                while (startPosition > 0 && (Char.IsNumber(_expression[startPosition]) || _expression[startPosition] == ',' || _expression[startPosition] == '.'))
                {
                    --startPosition;
                }
                throw new SyntaxException(String.Format("Ошибка: {0} {1}\r\nПожалуйста, исправьте ошибку и повторите попытку", lastToken.GetTokenString, number.GetTokenString),
                    startPosition, _expressionIndex - 1);
            }
        }
    }
}
