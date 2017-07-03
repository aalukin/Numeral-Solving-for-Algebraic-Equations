using System;
using System.Collections.Generic;
using TokenLibrary;

namespace PolishNotationLibrary
{
    /// <summary>
    /// Постфиксная запись уравнения
    /// </summary>
    public class PostfixNotation
    {
        /// <summary>
        /// Конструктор постфиксной записи уравнения
        /// </summary>
        /// <param name="inputstring"> Входная строка с выражением </param>
        public PostfixNotation(string inputstring)
        {
            Parser expressionParser = new Parser(inputstring);
            _infixTokenList = expressionParser.GetTokenList;
            _postfixTokenList = TransformationToInfixNotation(_infixTokenList);
            equalCalculation = new Calculation(this._postfixTokenList); 
        }

        /// <summary>
        /// Список лексем в инфиксной нотации
        /// </summary>
        List<Token> _infixTokenList;

        public string GetInfixNotation
        {
            get
            {
                string str = String.Empty;
                foreach (Token token in _infixTokenList)
                {
                    str += token.GetTokenString;
                }
                return str;
            }
        }

        /// <summary>
        /// Список лексем в постфиксной нотации
        /// </summary>
        public List<Token> _postfixTokenList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GetPostfixNotation
        {
            get
            {
                string str = String.Empty;
                foreach(Token token in _postfixTokenList)
                {
                    str = str + token.GetTokenString + ",";
                }
                return str;
            }
        }

        /// <summary>
        /// Преобразование из инфиксной в постфиксную нотацию
        /// </summary>
        /// <param name="inputTokenList"> Список лексем в инфисксной нотации </param>
        /// <returns> Список лексем в постфиксной нотации </returns>
        List<Token> TransformationToInfixNotation(List<Token> inputTokenList)
        {
            List<Token> result = new List<Token>();
            Stack<Token> operationStack = new Stack<Token>();
            foreach(Token token in inputTokenList) 
            {
                if (token is Number || token is Variable)
                {
                    result.Add(token);
                    continue;
                }

                if (token is Function || token is Bracket || token is Operation)
                {
                    if (operationStack.Count == 0)
                    {
                        operationStack.Push(token);
                        continue;
                    }

                    if (token is Function || (token is Bracket && ((Bracket)token).GetBracketType == Bracket.BracketType.OpenBracket))
                    {
                        operationStack.Push(token);
                        continue;
                    }

                    if (token is Bracket && ((Bracket)token).GetBracketType == Bracket.BracketType.CloseBracket)
                    {
                        Token stackPeek = operationStack.Peek();
                        while (!(stackPeek is Bracket && ((Bracket)stackPeek).GetBracketType == Bracket.BracketType.OpenBracket))
                        {
                            result.Add(operationStack.Pop());
                            stackPeek = operationStack.Peek();
                        }
                        operationStack.Pop();
                        if (operationStack.Count != 0 && operationStack.Peek() is Function)
                        {
                            result.Add(operationStack.Pop());
                        }
                        continue;
                    }

                    while (operationStack.Count != 0 && token.GetPriority <= operationStack.Peek().GetPriority)
                    {
                        result.Add(operationStack.Pop());
                    }
                    operationStack.Push(token);
                }
            }

            while (operationStack.Count != 0)
            {
                result.Add(operationStack.Pop());
            }

            return result;
        }

        /// <summary>
        /// Поле вычисления значения выражения в точке
        /// </summary>
        Calculation equalCalculation;

        /// <summary>
        /// Вычисление значения выражения в точке
        /// </summary>
        /// <param name="x"> Точка, значение выражения в которой нужно получить </param>
        /// <returns> Значения выражения в точке </returns>
        public double Calcuate(double x)
        {
            return equalCalculation.CalculateValueOfExpression(x);
        }

        /// <summary>
        /// Получить символ выбранной переменной
        /// </summary>
        public char GetVariableSymbol
        {
            get
            {
                return Variable.GetVariableSymbol;
            }
        }
    }
}
