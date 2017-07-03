using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Модифицированный метод Ньютона
    /// </summary>
    class ModifiedNewtonsMethod : NumeralMethod
    {
        /// <summary>
        /// Производная
        /// </summary>
        Derivative derivative;

        /// <summary>
        /// Значения первой прозводной
        /// </summary>
        double derivativeValue;

        /// <summary>
        /// Конструктор модифицированного метода Ньютона
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность решения</param>
        /// <param name="digitsCount"> Количество знаков после запятой </param>
        /// <param name="solvingReport"> Отчет </param>
        public ModifiedNewtonsMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport)
        {
            this.derivative = new Derivative(equation, epsilon);
        }

        /// <summary>
        /// Получения корня на подынтервале модифицированным методом Ньютона
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <returns> Корень на интервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            double leftBorderEquationValue = equation.Calcuate(leftBorder);
            double rightBorderEquationValue = equation.Calcuate(rightBorder);
            if (leftBorderEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(leftBorder), equation.GetVariableSymbol));
                return new Root(Round(leftBorder), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (rightBorderEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(rightBorder), equation.GetVariableSymbol));
                return new Root(Round(rightBorder), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(leftBorderEquationValue))
            {
                throw new DiscontinuityException(Round(leftBorder), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(rightBorderEquationValue))
            {
                throw new DiscontinuityException(Round(rightBorder), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            double lastX, nextX;
            solvingReport.AddBoldParagraph("Выбор начальной точки приближения:");
            if (base.equation.Calcuate(leftBorder) * this.derivative.Get2DX(leftBorder) >= 0)
            {
                solvingReport.AddParagraph(string.Format("Т.к. f(a) * f\"(a) = {0} * {1} {2} 0, то {3} = b точка начального приближения", leftBorderEquationValue, derivative.Get2DX(leftBorderEquationValue), '\u2265', equation.GetVariableSymbol));
                nextX = leftBorder;
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Т.к. f(a) * f\"(a) = {0} * {1} {2} 0, то {3} = b точка начального приближения", leftBorderEquationValue, derivative.Get2DX(leftBorderEquationValue), '\u2264', equation.GetVariableSymbol));
                nextX = rightBorder;
            }
            this.derivativeValue = derivative.GetDX(nextX);
            solvingReport.AddParagraph(string.Format("f'({0}0) = {1}", equation.GetVariableSymbol, derivativeValue));
            if (derivativeValue == 0)
            {
                solvingReport.AddParagraph("Производная равна 0");
                throw new FirstDirevativeException();
            }
            if (double.IsNaN(derivativeValue) || double.IsInfinity(derivativeValue))
            {
                solvingReport.AddParagraph("Производная не число");
                throw new FirstDirevativeException();
            }
            double lastEquationValue;
            solvingReport.CreateTable(4);
            solvingReport.AddTableCells(equation.GetVariableSymbol.ToString(), string.Format("f({0})", equation.GetVariableSymbol), "next" + equation.GetVariableSymbol, "delta");
            double count = 0;
            do
            {
                ++count;
                lastX = nextX;
                lastEquationValue = equation.Calcuate(lastX);
                solvingReport.AddTableCells(lastX.ToString(), lastEquationValue.ToString());
                if (lastEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(lastX), equation.GetVariableSymbol));
                    return new Root(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(lastEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(lastX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                nextX = lastX - (equation.Calcuate(lastX)) / this.derivativeValue;
                solvingReport.AddTableCells(nextX.ToString(), Math.Abs(lastX - nextX).ToString());
                if (lastX == nextX)
                {
                    break;
                }
            }
            while (Math.Abs(lastX - nextX) > base.epsilon  && count <= 1000);
            solvingReport.CloseTable();
            solvingReport.AddParagraph("Окургеление числа до заданной точности:");
            double middle = Round((lastX + nextX) / 2);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double midEquationValue = equation.Calcuate(middle);
            if (Math.Abs(midEquationValue) <= base.epsilon)
            {
                return new Root(Round(middle), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Модифицированный метод Ньютона сошелся в точке {0} = {1}, однако f({1}) = {2} => {0} = {1} не является корнем уравнения", equation.GetVariableSymbol, Round(middle), Round(midEquationValue)));
                if (double.IsNaN(midEquationValue) || double.IsInfinity(midEquationValue))
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