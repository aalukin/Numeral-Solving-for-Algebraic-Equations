using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема функция
    /// </summary>
    public class Function : Token
    {
        /// <summary>
        /// Список доступных функций
        /// </summary>
        public static readonly List<string> avaliableFunctions = new List<string>()
        {
            "SIN",
            "COS",
            "TAN",
            "ARCSIN",
            "ARCCOS",
            "ARCTAN",
            "LN",
            "SQRT",
            "SQR",
            "LG",
            "ABS"
        };

        /// <summary>
        /// Строковое представление лексемы
        /// </summary>
        string _functionString;

        /// <summary>
        /// Конструктор лексемы типа "функция"
        /// </summary>
        /// <param name="str"></param>
        public Function(string str) : base()
        {
            str = str.ToUpper();
            _functionString = str;
        }

        /// <summary>
        /// Получить строковое представление функции
        /// </summary>
        public override string GetTokenString
        {
            get
            {
                return _functionString;
            }
        }

        /// <summary>
        /// Выполнить функцию
        /// </summary>
        /// <param name="num"> Аргумент функции </param>
        /// <returns> Значение функции </returns>
        public double PerformFunction(double num)
        {
            switch (_functionString)
            {
                case ("SIN"):
                    {
                        return Math.Sin(num);
                    }
                case ("COS"):
                    {
                        return Math.Cos(num);
                    }
                case ("TAN"):
                    {
                        return Math.Tan(num);
                    }
                case ("ARCSIN"):
                    {
                        return Math.Asin(num);
                    }
                case ("ARCCOS"):
                    {
                        return Math.Acos(num);
                    }
                case ("ARCTAN"):
                    {
                        return Math.Atan(num);
                    }
                case ("LN"):
                    {
                        return Math.Log(num);
                    }
                case ("SQRT"):
                    {
                        return Math.Sqrt(num);
                    }
                case ("LG"):
                    {
                        return Math.Log10(num);
                    }
                case ("ABS"):
                    {
                        return Math.Abs(num);
                    }
                default:
                    {
                        throw new Exception("Неизвестная функция!");
                    }
            }
        }
    }
}
