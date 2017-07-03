using System;
using System.Collections.Generic;
using System.Linq;
using PolishNotationLibrary;
using System.Text;
using System.Threading.Tasks;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Численные методы нахождения производных разных порядков
    /// </summary>
    class Derivative
    {
        /// <summary>
        /// Уравнение
        /// </summary>
        PostfixNotation equation;

        /// <summary>
        /// Погрешность
        /// </summary>
        double epsilon;

        /// <summary>
        /// Конструктор производной
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность </param>
        public Derivative(PostfixNotation equation, double epsilon)
        {
            this.equation = equation;
            this.epsilon = epsilon;
        }

        /// <summary>
        /// Нахождения значения 1-й производной функции в точке
        /// </summary>
        /// <param name="x"> Точка </param>
        /// <returns> Значения 1-й производной в точке </returns>
        public double GetDX(double x)
        {
            double leftValue = this.equation.Calcuate(x - this.epsilon);
            double rightValue = this.equation.Calcuate(x + this.epsilon);
            double increasEpsilon = this.epsilon;
            while (leftValue == rightValue)
            {
                increasEpsilon *= 10;
                if (increasEpsilon == 1)
                {
                    break;
                }
                leftValue = this.equation.Calcuate(x - increasEpsilon);
                rightValue = this.equation.Calcuate(x + increasEpsilon);
            }
            double dx = (rightValue - leftValue) / (2 * increasEpsilon);
            return dx;
        }

        /// <summary>
        /// Нахождение значения 2-й производной функции в точке
        /// </summary>
        /// <param name="x"> Точка </param>
        /// <returns> Значение 2-й производной функции в точке </returns>
        public double Get2DX(double x)
        {
            double d2x = (GetDX(x + this.epsilon) - GetDX(x -  this.epsilon)) / (2 * epsilon);
            return d2x;
        }
    }
}
