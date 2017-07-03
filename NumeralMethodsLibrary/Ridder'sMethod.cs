using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод Риддерса
    /// </summary>
    class RiddersMethod : NumeralMethod
    {
        /// <summary>
        /// Конструктор метода Риддера
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность </param>
        /// <param name="digitsCount"> Количество цифр после разделителя </param>
        /// <param name="solvingReport"> Отчет </param>
        public RiddersMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport) { }

        /// <summary>
        /// Получение корня уравнения на подынтервале методом Риддера
        /// </summary>
        /// <param name="leftBorder"> Левая граница подынтервала </param>
        /// <param name="rightBorder"> Правая граница подынтервала </param>
        /// <returns> Корень уравнения по подынтервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            double oldX;
            double oldEquationValue;
            double lastX = leftBorder;
            double lastEquationValue = base.equation.Calcuate(lastX);
            if (lastEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(lastX), equation.GetVariableSymbol));
                return new Root(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(lastEquationValue))
            {
                throw new DiscontinuityException(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            double nextX = rightBorder;
            double betta; // Коэфициент метод Риддерса
            double bettaEquationValue;
            solvingReport.CreateTable(8);
            solvingReport.AddTableCells(string.Format("{0}[k-1]", equation.GetVariableSymbol), string.Format("{0}[k]", equation.GetVariableSymbol), string.Format("f({0}[k-1])", equation.GetVariableSymbol), string.Format("f({0}[k])", equation.GetVariableSymbol), "\u03B2", "f(\u03B2)", string.Format("{0}[k+1]", equation.GetVariableSymbol), "delta");
            int count = 0;
            do
            {
                ++count;
                oldX = lastX;
                oldEquationValue = lastEquationValue;
                lastX = nextX;
                lastEquationValue = base.equation.Calcuate(lastX);
                solvingReport.AddTableCells(oldX.ToString(), lastX.ToString(), oldEquationValue.ToString(), lastEquationValue.ToString());
                if (lastEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(lastX), equation.GetVariableSymbol));
                    return new Root(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(lastEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                betta = (oldX + lastX) / 2;
                bettaEquationValue = base.equation.Calcuate(betta);
                solvingReport.AddTableCells(betta.ToString(), bettaEquationValue.ToString());
                if (bettaEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(betta), equation.GetVariableSymbol));
                    return new Root(Round(betta), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(bettaEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(betta, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                nextX = betta + (betta - oldX) * Math.Sign(oldEquationValue - lastEquationValue) * bettaEquationValue / Math.Sqrt(Math.Pow(bettaEquationValue, 2) - oldEquationValue * lastEquationValue);
                solvingReport.AddTableCells(nextX.ToString(), Math.Abs(nextX - lastX).ToString());
            }
            while (Math.Abs(nextX - lastX) > base.epsilon && count <= 5000);
            solvingReport.CloseTable();
            double middle = (nextX + lastX) / 2;
            solvingReport.AddParagraph("Округление числа до заданной точноси:");
            middle = Round(middle);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double middleValue = equation.Calcuate(middle);
            if (Math.Abs(middleValue) <= base.epsilon)
            {
                return new Root(middle, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddPhrase(string.Format("Метод Риддера сошелся в точке {0} = {1}, f({0}) = {2}\nПожалуйста, проверьте условие применимости метода на этом интервале", equation.GetVariableSymbol, Round((nextX + lastX) / 2), Round(equation.Calcuate((nextX + lastX) / 2))));
                if (double.IsInfinity(middleValue) || double.IsNaN(middleValue))
                {
                    throw new DiscontinuityException(middle, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                else
                {
                    return new FakeRoot(middle, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
            }
        }
    }
}