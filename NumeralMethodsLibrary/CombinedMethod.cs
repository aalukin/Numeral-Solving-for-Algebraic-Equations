using System;
using System.Collections.Generic;
using System.Linq;
using PolishNotationLibrary;
using System.Text;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Комбинированный метод
    /// </summary>
    class CombinedMethod : NumeralMethod
    {
        /// <summary>
        /// Производная
        /// </summary>
        Derivative derivative;

        /// <summary>
        /// Конструктор комбинированного метода
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность решения уравнения</param>
        /// <param name="digitsCount"> Количество знаков после разделителя </param>
        /// <param name="solvingReport"> Отчет </param>
        public CombinedMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport)
        {
            this.derivative = new Derivative(equation, epsilon);
        }

        /// <summary>
        /// Получение корня на интервале комбинированным методом
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <returns> Корень уравнения на интервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            double leftX = leftBorder;
            double rightX = rightBorder;
            double leftEquationValue = equation.Calcuate(leftX);
            double rightEquationValue = equation.Calcuate(rightX);
            if (leftEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравения", Round(leftX), equation.GetVariableSymbol));
                return new Root(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (rightEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравения", Round(rightX), equation.GetVariableSymbol));
                return new Root(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(leftEquationValue))
            {
                throw new DiscontinuityException(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(rightEquationValue))
            {
                throw new DiscontinuityException(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            double leftDerivativeValue, rightDerivativeValue;
            double nextLeft, nextRight;
            solvingReport.CreateTable(7);
            solvingReport.AddTableCells("a", "b", "f(a)", "f(b)", "next a", "next b", "delta");
            do
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString());
                if (leftEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравения", Round(leftX), equation.GetVariableSymbol));
                    return new Root(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (rightEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравения", Round(rightX), equation.GetVariableSymbol));
                    return new Root(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(leftEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(rightEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (leftEquationValue * derivative.Get2DX(leftX) >= 0)
                {
                    nextLeft = leftX - (leftEquationValue * (leftX - rightX)) / (leftEquationValue - rightEquationValue);
                }
                else
                {
                    leftDerivativeValue = derivative.GetDX(leftX);
                    if (leftDerivativeValue == 0 || double.IsNaN(leftDerivativeValue) || double.IsInfinity(leftDerivativeValue))
                    {
                        throw new FirstDirevativeException();
                    }
                    nextLeft = leftX - leftEquationValue / leftDerivativeValue;
                }
                if (rightEquationValue * derivative.Get2DX(rightX) >= 0)
                {
                    nextRight = rightX - (rightEquationValue * (rightX - leftX)) / (rightEquationValue - leftEquationValue);
                }
                else
                {
                    rightDerivativeValue = derivative.GetDX(rightX);
                    if (rightDerivativeValue == 0 || double.IsNaN(rightDerivativeValue) || double.IsInfinity(rightDerivativeValue))
                    {
                        throw new FirstDirevativeException();
                    }
                    nextRight = rightX - rightEquationValue / rightDerivativeValue;
                }
                solvingReport.AddTableCells(nextLeft.ToString(), nextRight.ToString(), Math.Abs(rightX - leftX).ToString());
                leftX = nextLeft;
                rightX = nextRight;
                leftEquationValue = equation.Calcuate(leftX);
                rightEquationValue = equation.Calcuate(rightX);
            }
            while (Math.Abs(rightX - leftX) > epsilon);
            solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), equation.Calcuate(leftX).ToString(), equation.Calcuate(rightX).ToString(), string.Empty, string.Empty, Math.Abs(rightX - leftX).ToString());
            solvingReport.CloseTable();
            double middle = (rightX + leftX) / 2;
            solvingReport.AddParagraph("Округление числа до заданной точности:");
            middle = Round(middle);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double middleEquationValue = equation.Calcuate(middle);
            if (Math.Abs(middleEquationValue) <= epsilon)
            {
                return new Root(Round(middle), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddPhrase(string.Format("Комбинированный метод сошелся в точке {0} = {1}, f({1}) = {2}\nПожалуйста, проверьте условие применимости метода на этом интервале", equation.GetVariableSymbol, Round(middle), Round(middleEquationValue)));
                if (double.IsNaN(middleEquationValue) || double.IsInfinity(middleEquationValue))
                {
                    throw new DiscontinuityException(Round(middle), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);   
                }
                else
                {
                    return new FakeRoot(Round(middle), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
            }
        }
    }
}