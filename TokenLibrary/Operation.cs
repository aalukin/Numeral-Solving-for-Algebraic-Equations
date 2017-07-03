using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема операции
    /// </summary>
    public class Operation : Token
    {
        /// <summary>
        /// Доступные операции
        /// </summary>
        public static readonly string avaliableOperations = "+-*/^";

        /// <summary>
        /// Сивмол операции
        /// </summary>
        char _operationSymbol;

        /// <summary>
        /// Получить символ операции
        /// </summary>
        public char GetOperationSymbol
        {
            get
            {
                return _operationSymbol;
            }
        }

        /// <summary>
        /// Приоритет опрерации
        /// </summary>
        int _operationPriority;

        /// <summary>
        /// Получить приоритет операции
        /// </summary>
        public override int GetPriority
        {
            get
            {
                return _operationPriority;
            }
        }

        /// <summary>
        /// Конструктор лексемы типа "операция"
        /// </summary>
        /// <param name="ch"> Символ операции </param>
        public Operation(char ch) : base()
        {
            _operationSymbol = ch;
            _operationPriority = FindOperationPriority(ch);
        }

        /// <summary>
        /// Определить приоритет опреации
        /// </summary>
        /// <param name="ch"> Символ операции </param>
        /// <returns> Приоритет операции </returns>
        int FindOperationPriority(char ch)
        {
            switch (ch)
            {
                case ('+'):
                case ('-'):
                    {
                        return 1;
                    }
                case ('*'):
                case ('/'):
                    {
                        return 2;
                    }
                case ('^'):
                    {
                        return 3;
                    }
                default:
                    {
                        throw new Exception("Не опрерация!");
                    }
            }
        }

        /// <summary>
        /// Получить строковое представление операции
        /// </summary>
        public override string GetTokenString
        {
            get
            {
                return _operationSymbol.ToString();
            }
        }

        /// <summary>
        /// Выполнить операцию
        /// </summary>
        /// <param name="num2"> Правый операнд в инфиксной записи </param>
        /// <param name="num1"> Левый операнд в инфиксной записи </param>
        /// <returns></returns>
        public double PerformOperation(double num2, double num1)
        {
            switch (_operationSymbol)
            {
                case ('+'):
                    {
                        return num1 + num2;
                    }
                case ('-'):
                    {
                        return num1 - num2;
                    }
                case ('*'):
                    {
                        return num1 * num2;
                    }
                case ('/'):
                    {
                        return num1 / num2;
                    }
                case ('^'):
                    {
                        return Math.Pow(num1, num2);
                    }
                default:
                    {
                        throw new Exception("Неизвестная операция");
                    }
            }
        }
    }
}
