using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Исключение, возникающее при неопределенности значения функции в точке
    /// </summary>
    class DiscontinuityException : Exception
    {
        /// <summary>
        /// Точка разрыва
        /// </summary>
        double breakPoint;

        /// <summary>
        /// Левая граница интервала, на котором функция имеет разрыв
        /// </summary>
        double leftBorder;

        /// <summary>
        /// Правая граница интервала, на котором функция имеет разрыв
        /// </summary>
        double rightBorder;

        /// <summary>
        /// Выбранный символ переменной уравнения
        /// </summary>
        char equationVariable;

        /// <summary>
        /// Конструктор исключения разрыва
        /// </summary>
        /// <param name="breakPoint"> Точка разрыва </param>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <param name="equationVariable"> Выбранная переменная уравнения </param>
        public DiscontinuityException(double breakPoint, double leftBorder, double rightBorder, char equationVariable)
        {
            this.breakPoint = breakPoint;
            this.leftBorder = leftBorder;
            this.rightBorder = rightBorder;
            this.leftBorder = leftBorder;
            this.equationVariable = equationVariable;
        }

        /// <summary>
        /// Строковая инофрмация о точке разрыва
        /// </summary>
        /// <returns> Строка с информацией о точке разрыва  </returns>
        public override string ToString()
        {
            return String.Format("Точка разрыва {0} = {1}   интервал [{2}; {3}]", equationVariable, breakPoint, leftBorder, rightBorder);
        }
    }
}
