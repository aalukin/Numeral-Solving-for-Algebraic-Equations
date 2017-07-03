using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод Золотого сечения
    /// </summary>
    class GoldenSectrionMethod : NumeralMethod
    {
        /// <summary>
        /// Первое число Золотого сечения
        /// </summary>
        private static readonly double firstGoldenNumber = (3 - Math.Sqrt(5)) / 2;

        /// <summary>
        /// Второе число Золотого сечения
        /// </summary>
        private static readonly double secondGoldenNumber = (Math.Sqrt(5) - 1) / 2;

        /// <summary>
        /// Конструктор метода Золотого сечения
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="epsilon"> Погрешность </param>
        /// <param name="digitsCount"> Количество цифр после разделителя </param>
        /// <param name="solvingReport"> Отчет решения уравнения </param>
        public GoldenSectrionMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport) : base(equation, epsilon, digitsCount, solvingReport) { }

        /// <summary>
        /// Получение корня уравнения на подынтервале методом Золотого сечения
        /// </summary>
        /// <param name="leftBorder"> Левая граница подынтервала </param>
        /// <param name="rightBorder"> Правая граница подынтервала </param>
        /// <returns> Корень уравнения на подынтервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            double startLeftBorder = leftBorder,
                startRightBorder = rightBorder,
                leftBorderEquationValue,
                rightBorderEquationValue,
                firstGoldenPoint,
                secondGoldenPoint,
                firstGoldenPointEquationValue,
                secondGoldenPointEquationValue;
            solvingReport.CreateTable(9);
            solvingReport.AddTableCells("a", "b", "f(a)", "f(b)", "u1", "u2", "f(u1)", "f(u2)", "delta");
            int count = 0;
            do
            {
                leftBorderEquationValue = equation.Calcuate(leftBorder);
                rightBorderEquationValue = equation.Calcuate(rightBorder);
                solvingReport.AddTableCells(leftBorder.ToString(), rightBorder.ToString(), leftBorderEquationValue.ToString(), rightBorderEquationValue.ToString());
                if (leftBorderEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(leftBorder), equation.GetVariableSymbol));
                    return new Root(Round(leftBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (rightBorderEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(rightBorder), equation.GetVariableSymbol));
                    return new Root(Round(rightBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(leftBorderEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(leftBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(rightBorderEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(rightBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                firstGoldenPoint = leftBorder + firstGoldenNumber * (rightBorder - leftBorder);
                secondGoldenPoint = leftBorder + secondGoldenNumber * (rightBorder - leftBorder);
                firstGoldenPointEquationValue = equation.Calcuate(firstGoldenPoint);
                secondGoldenPointEquationValue = equation.Calcuate(secondGoldenPoint);
                solvingReport.AddTableCells(firstGoldenPoint.ToString(), secondGoldenPoint.ToString(), firstGoldenPointEquationValue.ToString(), secondGoldenPointEquationValue.ToString(), Math.Abs(rightBorder - leftBorder).ToString());
                if (firstGoldenPointEquationValue == 0)
                {
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(firstGoldenPoint), equation.GetVariableSymbol));
                    return new Root(Round(firstGoldenPoint), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (secondGoldenPointEquationValue == 0)
                {
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(secondGoldenPoint), equation.GetVariableSymbol));
                    return new Root(Round(secondGoldenPoint), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(firstGoldenPointEquationValue))
                {
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(firstGoldenPoint), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(secondGoldenPointEquationValue))
                {
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(secondGoldenPoint), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (leftBorderEquationValue * firstGoldenPointEquationValue <= 0)
                {
                    if (rightBorderEquationValue * secondGoldenPointEquationValue <= 0 && Math.Abs(leftBorder - firstGoldenPoint) >= Math.Abs(rightBorder - secondGoldenPoint))
                    {
                        if (firstGoldenPointEquationValue * secondGoldenPointEquationValue <= 0 && Math.Abs(rightBorder - secondGoldenPoint) >= Math.Abs(firstGoldenPoint - secondGoldenPoint))
                        {
                            leftBorder = firstGoldenPoint;
                            rightBorder = secondGoldenPoint;
                        }
                        else
                        {
                            leftBorder = secondGoldenPoint;
                        }
                    }
                    else
                    {
                        if (firstGoldenPointEquationValue * secondGoldenPointEquationValue <= 0 && Math.Abs(leftBorder - firstGoldenPoint) >= Math.Abs(firstGoldenPoint - secondGoldenPoint))
                        {
                            leftBorder = firstGoldenPoint;
                            rightBorder = secondGoldenPoint;
                        }
                        else
                        {
                            rightBorder = firstGoldenPoint;
                        }
                    }
                    
                }
                else
                {
                    if (rightBorderEquationValue * secondGoldenPointEquationValue <= 0)
                    {
                        if (firstGoldenPointEquationValue * secondGoldenPointEquationValue <= 0 && Math.Abs(rightBorder - secondGoldenPoint) >= Math.Abs(firstGoldenPoint - secondGoldenPoint))
                        {
                            leftBorder = firstGoldenPoint;
                            rightBorder = secondGoldenPoint;
                        }
                        else
                        {
                            leftBorder = secondGoldenPoint;
                        }
                    }
                    else
                    {
                        if (firstGoldenPointEquationValue * secondGoldenPointEquationValue <= 0)
                        {
                            leftBorder = firstGoldenPoint;
                            rightBorder = secondGoldenPoint;
                        }
                        else
                        {
                            if (leftBorderEquationValue * secondGoldenPointEquationValue <= 0 || rightBorderEquationValue * firstGoldenPointEquationValue <= 0)
                            {
                                if (leftBorderEquationValue * secondGoldenPointEquationValue <= 0)
                                {
                                    if (rightBorderEquationValue * firstGoldenPointEquationValue <= 0 && Math.Abs(leftBorderEquationValue - secondGoldenPoint) >= Math.Abs(rightBorder - firstGoldenPoint))
                                    {
                                        leftBorder = firstGoldenPoint;
                                    }
                                    else
                                    {
                                        rightBorder = secondGoldenPoint;
                                    }  
                                }
                                else
                                {
                                    leftBorder = firstGoldenPoint;
                                }
                            }
                            else
                            {
                                solvingReport.CloseTable();
                                solvingReport.AddParagraph("Найденные точки приближения не соответсвуют условиям сходимости метода Золотого сечения");
                                throw new MethodIsNotAplicableException(Round(startLeftBorder), Round(startRightBorder));
                            }
                        }
                    }
                }
                ++count;
            }
            while (Math.Abs(rightBorder - leftBorder) > epsilon && count <= 1000);
            solvingReport.AddTableCells(leftBorder.ToString(), rightBorder.ToString(), equation.Calcuate(leftBorder).ToString(), equation.Calcuate(rightBorder).ToString(), string.Empty, string.Empty, string.Empty, string.Empty, Math.Abs(leftBorder - rightBorder).ToString());
            solvingReport.CloseTable();
            double middle = (leftBorder + rightBorder) / 2;
            solvingReport.AddParagraph(string.Format("(a + b) / 2 = {0}", middle));
            solvingReport.AddParagraph("Округление числа до заданной точности:");
            middle = Round(middle);
            double middleEquactionValue = equation.Calcuate(middle);
            solvingReport.AddParagraph(string.Format("(a + b) / 2 = {0}", middleEquactionValue));
            if (Math.Abs(middleEquactionValue) <= epsilon)
            {
                return new Root(middle, Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddParagraph(string.Format("Метод Золотого сечения сошелся в точке {0} = {1}, однако f({1}) = {2} \nПожалуйста, проверьте условие применимости метода на этом интервале", equation.GetVariableSymbol, middle, middleEquactionValue));
                if (double.IsNaN(equation.Calcuate((leftBorder + rightBorder) / 2)) || double.IsInfinity(equation.Calcuate((leftBorder + rightBorder) / 2)))
                {
                    throw new DiscontinuityException(middle, Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                else
                {
                    return new FakeRoot(middle, Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
            }
        }
    }
}
