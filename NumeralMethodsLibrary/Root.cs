using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Корень уравнения
    /// </summary>
    public class Root
    {
        /// <summary>
        /// Значеник корня
        /// </summary>
        double rootValue;

        /// <summary>
        /// Получить значение корня
        /// </summary>
        public double GetRootValue
        {
            get
            {
                return this.rootValue;
            }
        }

        /// <summary>
        /// Левая граница интервала, на котором был найден корень
        /// </summary>
        double intervalLeftBorder;

        /// <summary>
        /// Правая граница интервала, на которым был найдн корень
        /// </summary>
        double intervalRightBorder;

        /// <summary>
        /// Символ выбранной переменной
        /// </summary>
        char variableSymbol;

        /// <summary>
        /// Конструктор корня уравнения
        /// </summary>
        /// <param name="rootValue"> Значение корня </param>
        /// <param name="leftBorder"> Левая граница интервала, на котором был найден корень </param>
        /// <param name="rightBorder"> Правая гарница интервала, на котором был найден корень </param>
        /// <param name="variableSymbol"> Символ выбранной переменной </param>
        public Root(double rootValue, double leftBorder, double rightBorder, char variableSymbol)
        {
            this.rootValue = rootValue;
            this.intervalLeftBorder = leftBorder;
            this.intervalRightBorder = rightBorder;
            this.variableSymbol = variableSymbol;
        }

        /// <summary>
        /// Информация о найденном корне
        /// </summary>
        /// <returns> Строка с информацией о найденном корне </returns>
        public override string ToString()
        {
            return String.Format("{0}, интервал {1} {2} [{3}; {4}]", this.rootValue, this.variableSymbol , '\u2208', this.intervalLeftBorder, this.intervalRightBorder);
        }
    }
}