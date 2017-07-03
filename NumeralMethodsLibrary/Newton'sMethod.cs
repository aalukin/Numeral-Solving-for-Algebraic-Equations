using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод Ньютона
    /// </summary>
    class NewtonsMethod : NumeralMethod
    {
        /// <summary>
        /// Производная
        /// </summary>
        Derivative derivative;

        /// <summary>
        /// Конструктор метода Ньютона
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность </param>
        /// <param name="digitCount"> Количество цифр после разделителя </param>
        /// <param name="solvingReport"> Отчет решения уравнения </param>
        public NewtonsMethod(PostfixNotation equation, double epsilon, int digitCount, Report solvingReport) : base(equation, epsilon, digitCount, solvingReport)
        {
            this.derivative = new Derivative(equation, epsilon);
        }

        /// <summary>
        /// Получение корня на интервале методом Ньютона
        /// </summary>
        /// <param name="leftborder"> Левая граница подынтервала для решения уравнения </param>
        /// <param name="rightborder"> Правая граница подынтервала для решения уравнения </param>
        /// <returns> Корень уравнения на подыинтервале </returns>
        public override Root GetRoot(double leftborder, double rightborder)
        {
            double lastX, nextX;
            double leftEquationValue = equation.Calcuate(leftborder);
            double rightEquationValue = equation.Calcuate(rightborder);
            if (leftEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0, => {1} = {0} можно считать корнем уравнения", Round(leftborder), equation.GetVariableSymbol));
                return new Root(Round(leftborder), Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
            }
            if (rightEquationValue == 0)
            {
                solvingReport.AddParagraph(string.Format("f({0}) = 0, => {1} = {0} можно считать корнем уравнения", Round(rightborder), equation.GetVariableSymbol));
                return new Root(Round(rightborder), Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(leftEquationValue))
            {
                throw new DiscontinuityException(Round(leftborder), Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(rightEquationValue))
            {
                throw new DiscontinuityException(Round(rightborder), Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
            }
            solvingReport.AddBoldPhrase("Выбор начального приближения:");
            double lastEquationValue;
            if (leftEquationValue * this.derivative.Get2DX(leftborder) >= 0)
            {
                solvingReport.AddParagraph(string.Format("Т.к. f(a) * f\"(a) = {0} * {1} {2} 0, то {3} = a начальная точка приближения", leftEquationValue, derivative.Get2DX(leftborder), '\u2265', equation.GetVariableSymbol));
                nextX = leftborder;
                lastEquationValue = leftEquationValue;
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Т.к. f(a) * f\"(a) = {0} * {1} {2} 0, то {3} = b точка начального приближения", leftEquationValue, derivative.Get2DX(leftborder), '\u2264', equation.GetVariableSymbol));
                nextX = rightborder;
                lastEquationValue = rightEquationValue;
            }
            solvingReport.CreateTable(5);
            solvingReport.AddTableCells(equation.GetVariableSymbol.ToString(), string.Format("f({0})", equation.GetVariableSymbol.ToString()), string.Format("f\"({0})", equation.GetVariableSymbol), "next" + equation.GetVariableSymbol, "delta");
            do
            {
                lastX = nextX;
                lastEquationValue = this.equation.Calcuate(lastX);
                solvingReport.AddTableCells(lastX.ToString(), lastEquationValue.ToString());
                if (lastEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) == 0 => {1} = {0} можно считать корнем уравнения", Round(lastX), equation.GetVariableSymbol));
                    return new Root(Round(lastX), Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
                }
                double derivariveValue = this.derivative.GetDX(lastX);
                solvingReport.AddTableCells(derivariveValue.ToString());
                if (derivariveValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph("Производная равна 0");
                    throw new FirstDirevativeException();
                }
                if (double.IsNaN(derivariveValue) || double.IsInfinity(derivariveValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph("Производная не число");
                    throw new FirstDirevativeException();
                }
                nextX = lastX - (lastEquationValue / derivariveValue);
                solvingReport.AddTableCells(nextX.ToString(), Math.Abs(lastX - nextX).ToString());
            }
            while (Math.Abs(lastX - nextX) > base.epsilon);
            solvingReport.CloseTable();
            solvingReport.AddParagraph("Окургеление числа до заданной точности:");
            nextX = Round(nextX);
            double nextValue = equation.Calcuate(nextX);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, nextX));
            if (Math.Abs(nextValue) <= epsilon)
            {
                return new Root(nextX, Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Метод Ньютона сошелся в точке {0} = {1}, однако f({1}) = {2} => {0} = {1} не является корнем уравнения", equation.GetVariableSymbol, Round(nextX), Round(equation.Calcuate(nextX))));
                if (double.IsNaN(nextValue) || double.IsInfinity(nextValue))
                {
                    throw new DiscontinuityException(nextX, Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
                }
                else
                {
                    return new FakeRoot(nextX, Round(leftborder), Round(rightborder), equation.GetVariableSymbol);
                }
            }
        }
    }
}
