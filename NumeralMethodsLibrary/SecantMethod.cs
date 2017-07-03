using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод секущих
    /// </summary>
    class SecantMethod : NumeralMethod
    {
        /// <summary>
        /// Конструктор метода секущих
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность решения уравнения </param>
        /// <param name="digitsCount"> Количество цифр после запятой</param>
        public SecantMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport) { }

        /// <summary>
        /// Получения корня на интервале методом секущих
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <returns> Корень уравнения на интервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            double oldX = leftBorder;
            double lastX = leftBorder + 2 * epsilon;
            double nextX;
            double oldEquationValue = base.equation.Calcuate(oldX);
            if (oldEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(oldX), equation.GetVariableSymbol));
                return new Root(Round(oldX), Round(leftBorder), Round(rightBorder), base.equation.GetVariableSymbol);
            }
            if (double.IsNaN(oldEquationValue))
            {
                throw new DiscontinuityException(Round(oldX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            double lastEquationValue;
            solvingReport.CreateTable(6);
            solvingReport.AddTableCells(string.Format("{0}(n-1)", equation.GetVariableSymbol), string.Format("f({0}(n-1))", equation.GetVariableSymbol), string.Format("{0}n", equation.GetVariableSymbol), string.Format("f({0}n)", equation.GetVariableSymbol), string.Format("{0}(n+1)", equation.GetVariableSymbol), "delta");
            do
            {
                solvingReport.AddTableCells(oldX.ToString(), oldEquationValue.ToString(), lastX.ToString());
                lastEquationValue = base.equation.Calcuate(lastX);
                solvingReport.AddTableCells(lastEquationValue.ToString());
                if (lastEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(lastX), equation.GetVariableSymbol));
                    return new Root(Round(lastX), Round(leftBorder), Round(rightBorder), base.equation.GetVariableSymbol);
                }
                if (double.IsNaN(lastEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                nextX = oldX - (oldEquationValue * (lastX - oldX)) / (lastEquationValue - oldEquationValue);
                solvingReport.AddTableCells(nextX.ToString());
                oldX = lastX;
                oldEquationValue = lastEquationValue;
                lastX = nextX;
                solvingReport.AddTableCells((Math.Abs(oldX - lastX)).ToString());
            }
            while (Math.Abs(oldX - lastX) > base.epsilon);
            solvingReport.CloseTable();
            double middle = (oldX + lastX) / 2;
            solvingReport.AddParagraph("Окургеление числа до заданной точности:");
            middle = Round(middle);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double midEquationValue = equation.Calcuate(middle);
            if (Math.Abs(midEquationValue) <= base.epsilon)
            {
                return new Root(middle, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Метод секущих сошелся в точке {0} = {1}, однако f({1}) = {2} => данная точка не является корнем", equation.GetVariableSymbol, Round(middle), Round(midEquationValue)));
                if (double.IsNaN(midEquationValue) || double.IsInfinity(midEquationValue))
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