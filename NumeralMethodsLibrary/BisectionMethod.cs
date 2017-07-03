using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Метод половинного деления
    /// </summary>
    class BisectionMethod : NumeralMethod
    {
        /// <summary>
        /// Конструктор метода половинного деления
        /// </summary>
        /// <param name="equation"> Уравнение </param>
        /// <param name="accuracy"> Точность </param>
        /// <param name="digitsCount"> Количество цифр после разделитеся </param>
        /// <param name="solvingReport"> Отчет по решению уравнения </param>
        public BisectionMethod(PostfixNotation equation, double accuracy, int digitsCount, Report solvingReport) : base(equation, accuracy, digitsCount, solvingReport) { }

        /// <summary>
        /// Получение корня на подытервале методом половинного деления
        /// </summary>
        /// <param name="leftBorder"> Левая граница подынтервала </param>
        /// <param name="rightBorder"> Правая граница подынтервала </param>
        /// <returns> Корень уравнения на подынтервале </returns>
        public override Root GetRoot(double leftBorder, double rightBorder)
        {
            solvingReport.CreateTable(7);
            solvingReport.AddTableCells("a", "b", "f(a)", "f(b)", "c", "f(c)", "delta");
            double startLeftBorder = leftBorder;
            double startRightBorder = rightBorder;
            double midOfInterval = (leftBorder + rightBorder) / 2;
            double leftEquationValue = equation.Calcuate(leftBorder);
            double rightEquationValue = equation.Calcuate(rightBorder);
            double midEquationValue = equation.Calcuate(midOfInterval);
            if (Math.Abs(rightBorder - leftEquationValue) <= epsilon)
            {
                solvingReport.AddTableCells(leftBorder.ToString(), rightBorder.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString(), midOfInterval.ToString(), midEquationValue.ToString(), Math.Abs(rightBorder - leftBorder).ToString());
            }
            while (Math.Abs(rightBorder - leftBorder) > epsilon)
            {
                leftEquationValue = equation.Calcuate(leftBorder);
                rightEquationValue = equation.Calcuate(rightBorder);
                solvingReport.AddTableCells(leftBorder.ToString(), rightBorder.ToString(), leftEquationValue.ToString(), rightEquationValue.ToString());
                if (leftEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(leftBorder), equation.GetVariableSymbol));
                    return new Root(Round(leftBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (rightEquationValue == 0)
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(rightBorder), equation.GetVariableSymbol));
                    return new Root(Round(rightBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(leftEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(leftBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (double.IsNaN(rightEquationValue))
                {
                    solvingReport.AddTableCells(string.Empty, string.Empty, string.Empty);
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(rightBorder), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                midOfInterval = (leftBorder + rightBorder) / 2;
                midEquationValue = equation.Calcuate(midOfInterval);
                solvingReport.AddTableCells(midOfInterval.ToString(), midEquationValue.ToString(), Math.Abs(rightBorder - leftBorder).ToString());
                if (double.IsNaN(midEquationValue))
                {
                    solvingReport.CloseTable();
                    throw new DiscontinuityException(Round(midOfInterval), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (midEquationValue == 0)
                {
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(string.Format("f({0}) = 0, следовательно {1} = {0} можно считать корнем уравнения", Round(midOfInterval), equation.GetVariableSymbol));
                    return new Root(Round(midOfInterval), Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
                }
                if (leftBorder == midOfInterval || rightBorder == midOfInterval)
                {
                    break;
                }
                if (leftEquationValue * midEquationValue > 0)
                {
                    leftBorder = midOfInterval;
                }
                else
                {
                    rightBorder = midOfInterval;
                }
            }
            solvingReport.AddTableCells(leftBorder.ToString(), rightBorder.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, Math.Abs(rightBorder - leftBorder).ToString());
            solvingReport.CloseTable();
            solvingReport.AddParagraph(string.Format("(a + b) / 2 = {0}", ((leftBorder + rightBorder) / 2).ToString()));
            solvingReport.AddParagraph("Округление числа до заданной точности:");
            double middle = Round((leftBorder + rightBorder) / 2);
            solvingReport.AddParagraph(string.Format("{0} = {1}", equation.GetVariableSymbol, middle));
            double middleValue = equation.Calcuate(middle);
            if (Math.Abs(middleValue) <= epsilon)
            {
                return new Root(middle, Round(startLeftBorder), Round(startRightBorder), equation.GetVariableSymbol);
            }
            else
            {
                solvingReport.AddPhrase(string.Format("Метод половинного деления сошелся в точке {0} = {1}, f({0}) = {2}\nПожалуйста, проверьте условие применимости метода на этом интервале", equation.GetVariableSymbol, middle, Round(equation.Calcuate((rightBorder + leftBorder) / 2))));
                if (double.IsNaN(middleValue) || double.IsInfinity(middleValue))
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
