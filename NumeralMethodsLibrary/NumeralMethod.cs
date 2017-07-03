using System;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Перечисление численныз методов решения уравнений
    /// </summary>
    public enum NumeralMethods
    {
        /// <summary>
        /// Значение метода бисекции
        /// </summary>
        BisectionMethod,
        /// <summary>
        /// Значение метода золотого сечения
        /// </summary>
        GoldSectionMethod,
        /// <summary>
        /// Значение метода Ньютона
        /// </summary>
        NewtonsMethod,
        /// <summary>
        /// Значение модифицированного метода Ньютона
        /// </summary>
        ModifiedNewtonsMethod,
        /// <summary>
        /// Значение метода секущих
        /// </summary>
        SecantMethod,
        /// <summary>
        /// Значение метода хорд
        /// </summary>
        ChordMethod,
        /// <summary>
        /// Значение комбинированного метода хорд и касательных
        /// </summary>
        CombinedMethod,
        /// <summary>
        /// Значение метода Риддера
        /// </summary>
        RiddersMethod
    }

    /// <summary>
    /// Численный метод решения уравнения
    /// </summary>
    abstract class NumeralMethod
    {
        /// <summary>
        /// Уравнение
        /// </summary>
        protected PostfixNotation equation;

        /// <summary>
        /// Погрешность решения
        /// </summary>
        protected double epsilon;

        /// <summary>
        /// Количество знаков после запятой
        /// </summary>
        int digitsCount;

        /// <summary>
        /// Отчет по решению уравнения
        /// </summary>
        protected Report solvingReport;

        /// <summary>
        /// Конструктор численного метода решения уравнения
        /// </summary>
        /// <param name="equation"> Уравнение в инверсной записи </param>
        /// <param name="epsilon"> Погрешность решения уравнения </param>
        /// <param name="digitsCount"> Количество знаков после заяптой </param>
        /// <param name="solvingReport"> Отчет по решению уравнения </param>
        public NumeralMethod(PostfixNotation equation, double epsilon, int digitsCount, Report solvingReport)
        {
            this.equation = equation;
            this.epsilon = epsilon;
            this.digitsCount = digitsCount;
            this.solvingReport = solvingReport;
        }

        /// <summary>
        /// Численный метод решения уравнения
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала поиска решения </param>
        /// <param name="rightBorder"> Правая граница интервала поиска решения </param>
        /// <returns> Корень уравнения на интервале </returns>
        public abstract Root GetRoot(double leftBorder, double rightBorder);

        /// <summary>
        /// Определение существования корня на интервале
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала поиска решения </param>
        /// <param name="rightBorder"> Правая граница интервала посика решения </param>
        /// <returns> Интервал содержит корень уравнения - истина, иначе - ложь </returns>
        public bool ThisIntervalContainRoot(double leftBorder, double rightBorder)
        {
            double product = equation.Calcuate(leftBorder) * equation.Calcuate(rightBorder);
            if (product <= 0)
            {
                if (double.IsNaN(equation.Calcuate(leftBorder)))
                {
                    throw new DiscontinuityException(double.IsNaN(equation.Calcuate(leftBorder)) ? leftBorder : rightBorder, leftBorder, rightBorder, equation.GetVariableSymbol);
                }
                solvingReport.AddParagraph(string.Format("f(a) * f(b) = {0} {1} 0 => интервал содержит корень", product, '\u2264'));
                return true;
            }
            if (double.IsNaN(product))
            {
                solvingReport.AddParagraph(string.Format("f(a) * f(b) = {0} => интервал не содержит корня", product));
            }
            else
            {
                solvingReport.AddParagraph(string.Format("f(a) * f(b) = {0} > 0 => интервал не содержит корня", product));
            }
            solvingReport.AddPhrase("\n");
            return false;
        }

        /// <summary>
        /// Округление числа до необходмого количества знаков после разделителя
        /// </summary>
        /// <param name="x"> Корень уравнения </param>
        /// <returns> Округленный корень уравнения </returns>
        protected double Round(double x)
        {
            if (x >= 0)
            {
                return Math.Round(x + this.epsilon / 10, this.digitsCount);
            }
            else
            {
                return Math.Round(x - this.epsilon / 10, this.digitsCount);
            }
        }
    }
}
