using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод хорд
    /// </summary>
    class ChordMethod : NumeralMethod
    {

        /// <summary>
        /// Конструктор метода хорд
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность </param>
        /// <param name="digitsCount"> Количество цифр после разделителч </param>
        /// <param name="solvingReport"> Отчет </param>
        public ChordMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport)
        {
        }

        /// <summary>
        /// Получение корня методом хорд
        /// </summary>
        /// <param name="leftBorder"> Левая граница инетрвала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <returns> Найденный на интервале корень </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            solvingReport.CreateTable(7);
            solvingReport.AddTableCells("a", "b", "f(a)", "f(b)", string.Format("next{0}", equation.GetVariableSymbol), string.Format("f(next{0})", equation.GetVariableSymbol), "delta");
            double leftX = leftBorder;
            double rightX = rightBorder;
            double leftEquationValue = equation.Calcuate(leftX);
            double rightEquationValue = equation.Calcuate(rightX);
            if (leftEquationValue == 0)
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString(), string.Empty, string.Empty, string.Empty);
                solvingReport.CloseTable();
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(leftX), equation.GetVariableSymbol));
                return new Root(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (rightEquationValue == 0)
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString(), string.Empty, string.Empty, string.Empty);
                solvingReport.CloseTable();
                solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(rightX), equation.GetVariableSymbol));
                return new Root(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(leftEquationValue))
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString(), string.Empty, string.Empty, string.Empty);
                solvingReport.CloseTable();
                throw new DiscontinuityException(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            if (double.IsNaN(rightEquationValue))
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString(), string.Empty, string.Empty, string.Empty);
                solvingReport.CloseTable();
                throw new DiscontinuityException(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            double nextX, nextEquationValue;
            do
            {
                solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString());
                nextX = leftX - (leftEquationValue * (rightX - leftX)) / (rightEquationValue - leftEquationValue);
                nextEquationValue = equation.Calcuate(nextX);
                solvingReport.AddTableCells(nextX.ToString(), nextEquationValue.ToString(), Math.Abs(leftX - rightX).ToString());
                if (nextEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0 => {1} = {0} можно считать корнем уравнения", Round(nextX), equation.GetVariableSymbol));
                    return new Root(Round(nextX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(nextEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(nextX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                }
                if (leftEquationValue * nextEquationValue <= 0)
                {
                    if (rightX == nextX)
                    {
                        solvingReport.CloseTable();
                        solvingReport.AddParagraph("Округление числа: ");
                        rightX = Round(rightX);
                        solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, rightX));
                        rightEquationValue = equation.Calcuate(rightX);
                        if (Math.Abs(rightEquationValue) <= epsilon)
                        {
                            return new Root(rightX, Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                        }
                        else
                        {
                            solvingReport.AddParagraph(string.Format("Метод хорд сошелся в точке {0} = {1}, однако f({1}) = {2} => данная точка не является корнем", equation.GetVariableSymbol, rightX, rightEquationValue));
                            if (double.IsNaN(rightEquationValue) || double.IsInfinity(rightEquationValue))
                            {
                                throw new DiscontinuityException(Round(rightX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                            }
                            else
                            {
                                return new FakeRoot(Round(rightX), Round(leftBorder), Round(rightX), equation.GetVariableSymbol);
                            }
                        }
                    }
                    rightX = nextX;
                    rightEquationValue = nextEquationValue;
                }
                else
                {
                    if (leftX == nextX)
                    {
                        solvingReport.CloseTable();
                        solvingReport.AddParagraph("Округление числа: ");
                        leftX = Round(leftX);
                        solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, leftX));
                        leftEquationValue = equation.Calcuate(leftX);  
                        if (Math.Abs(leftEquationValue) <= epsilon)
                        {
                            return new Root(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                        }
                        else
                        {
                            solvingReport.AddParagraph(string.Format("Метод хорд сошелся в точке {0} = {1}, однако f({1}) = {2} => данная точка не является корнем", equation.GetVariableSymbol, leftX, leftEquationValue));
                            if (double.IsNaN(leftEquationValue) || double.IsInfinity(leftEquationValue))
                            {
                                throw new DiscontinuityException(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                            }
                            else
                            {
                                return new FakeRoot(Round(leftX), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
                            }
                        }
                    }
                    leftX = nextX;
                    leftEquationValue = nextEquationValue;
                }
            }
            while (Math.Abs(rightX - leftX) > epsilon);
            solvingReport.AddTableCells(leftX.ToString(), rightX.ToString(), equation.Calcuate(leftX).ToString(), equation.Calcuate(rightX).ToString(), string.Empty, string.Empty, Math.Abs(rightX - leftX).ToString());
            solvingReport.CloseTable();
            double middle = (leftX + rightX) / 2;
            solvingReport.AddParagraph("Окургеление числа до заданной точности:");
            middle = Round(middle);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double middleEquationValue = equation.Calcuate(middle);
            if (Math.Abs(middleEquationValue) <= epsilon)
            {
                return new Root(Round(middle), Round(leftBorder), Round(rightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Метод хорд сошелся в точке {0} = {1}, однако f({1}) = {2} => данная точка не является корнем", equation.GetVariableSymbol, Round(middle), Round(middleEquationValue)));
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