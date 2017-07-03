using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема число
    /// </summary>
    public class Number : Token
    {
        /// <summary>
        /// Значение числа
        /// </summary>
        double _number;

        /// <summary>
        /// Получить значение числа
        /// </summary>
        public double GetValue
        {
            get
            {
                return _number;
            }
        }

        /// <summary>
        /// Конструктор объекта типа "число"
        /// </summary>
        /// <param name="numstr"></param>
        public Number(string numstr)
        {
            double.TryParse(numstr, out _number);
        }

        /// <summary>
        /// Получить строковое предстваление числа
        /// </summary>
        public override string GetTokenString
        {
            get
            {
                return _number.ToString();
            }
        }
    }
}
