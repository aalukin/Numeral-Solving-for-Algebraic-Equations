using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Исключение, возникающее в случае неприменимости численного метода решения уравнения на интервале
    /// </summary>
    class MethodIsNotAplicableException : Exception
    {

        /// <summary>
        /// Левая граница интервала
        /// </summary>
        double leftBorder;

        /// <summary>
        /// Правая граница интервала
        /// </summary>
        double rightBorder;

        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="point"> Точка, в которой соешлся метод </param>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        public MethodIsNotAplicableException (double leftBorder, double rightBorder)
        {
            this.leftBorder = leftBorder;
            this.rightBorder = rightBorder;
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <returns> Строка с информацией об ошибке </returns>
        public override string ToString()
        {
            return string.Format("Метод не применим на интервале [{0}; {1}]", leftBorder, rightBorder);
        }
    }
}