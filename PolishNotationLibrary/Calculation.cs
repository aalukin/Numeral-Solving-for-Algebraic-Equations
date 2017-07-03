using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenLibrary;

namespace PolishNotationLibrary
{
    /// <summary>
    /// Вычисление выражений
    /// </summary>
    public class Calculation
    {
        /// <summary>
        /// Конструктор вычисления значения функции в точке
        /// </summary>
        /// <param name="tokenList"> Список лексем в инверсной записе</param>
        public Calculation(List<Token> tokenList)
        {
            this.postfixexpression = tokenList;
        }

        /// <summary>
        /// Список токенов выражения
        /// </summary>
        List<Token> postfixexpression;

        /// <summary>
        /// Вычислить значение выражение в польской инверсной записи в точке
        /// </summary>
        /// <param name="varValue"> Значение перменной </param>
        /// <returns> Значение выражения в точке </returns>
        public double CalculateValueOfExpression(double varValue)
        {
            if (this.postfixexpression.Count == 0)
            {
                throw new Exception("Пустое выражение");
            }
            Stack<double> operandsStack = new Stack<double>();
            foreach (Token token in this.postfixexpression)
            {
                if (token is Number)
                {
                    operandsStack.Push(((Number)token).GetValue);
                    continue;
                }

                if (token is Variable)
                {
                    operandsStack.Push(varValue);
                    continue;
                }

                if (token is Operation)
                {
                    operandsStack.Push(((Operation)token).PerformOperation(operandsStack.Pop(), operandsStack.Pop()));
                    continue;
                }

                if (token is Function)
                {
                    operandsStack.Push(((Function)token).PerformFunction(operandsStack.Pop()));
                    continue;
                }
            }

            if (operandsStack.Count > 1)
            {
                throw new Exception();
            }

            return operandsStack.Peek();
        }
    }
}
