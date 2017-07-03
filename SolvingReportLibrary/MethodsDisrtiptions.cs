using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvingReportLibrary
{
    /// <summary>
    /// Описание численных методов
    /// </summary>
    public static class MethodsDisrtiptions
    {
        /// <summary>
        /// Добавление в отчет описания метода половинного деления
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddBisectionMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание алгоритма метода половинного деления:");
            solvingReport.AddParagraph(string.Format(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала
Уравнение содержит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
Пусть с -середина интервала [a, b]. c = (a + b) / 2
Вычиcлим значение f(c) и проверим следующие возможные ситуации:
    1. Если f(a) * f(c) {0} 0, то b = c
    2. Если f(b) * f(c) {0} 0, то a = c
    3. Если |f(c)| {0} {1}, где {1} - погрешность решения уравнения, то с - корень
Действие повторяется до тех пор, пока длина интервала [a, b] не станет {0} погрешности {1}, тогда с - корень уравнения", '\u2264', '\u03B5'));
            solvingReport.AddPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания метода Золотого сечения
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddGoldenSectionMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание метода Золотого сечения:");
            solvingReport.AddParagraph(string.Format(@"Пусть а - нижняя граница интервала, b - верхняя граница интервала
Уравнение содержит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
Точки u1 и u2 - промежуточные точки приближения функции", '\u2264'));
            solvingReport.AddImage(Properties.Resources.GoldenSectionFormulaImage, 100f, 50f);
            solvingReport.AddParagraph(string.Format(@"Для перехода к следующему шагу итерации выбираются те 2 ближайште точки из a, b, u1, u2,
значения функции в которых различаются знаками
Действие повторяется до тех пор, пока длина интервала [a, b] не станет {0} погрешности {1}
Тогда корень уравнения: (a + b) / 2", '\u2264', '\u03B5'));
            solvingReport.AddPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания метода Ньютона
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddNewtonsMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание метода Ньютона:");
            solvingReport.AddParagraph(string.Format(@"Пусть а - нижняя граница интервала, b - верхняя граница инетрвала
Уравнение содержит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
В качестве начальной точки приближения выбирается граничная точка, знак значения функции в которой совпадает со знаком второй производной
Пусть x - начальная точка приближения
Следующие точки вычисляются по итерационной формуле:", '\u2264'));
            solvingReport.AddImage(Properties.Resources.NewtonsMethodFormulaImage, 120f, 45f);
            solvingReport.AddParagraph(string.Format(@"Действие повторяется до тех пор, пока |Xi - Xi-1| > {0},
где {0} - погрешность решения уравнения", '\u03B5'));
            solvingReport.AddPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания модифицированного метода Ньютона
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddModifiedNewtonsMethidDiscriotion(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание модифицированного метода Ньютона:");
            solvingReport.AddParagraph(string.Format(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала
Уравнение соедржит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
В качестве начальной точки приближения выбиравется граничная точка, знак значения функции в которой совпадает со знаком втрой производной
Пусть X0 - начальная точка приближения
Следующие точки вычисляются по итерационнной формуле:", '\u2264'));
            solvingReport.AddImage(Properties.Resources.ModifiedNewtonsMethodImage, 120f, 45f);
            solvingReport.AddParagraph(string.Format(@"Действие повторяется до тех пор, пока |Xi - Xi-1| > {0},
где {0} - погрешность решения уравнения", '\u03B5'));
            solvingReport.AddPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания метода секущих
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddSecandMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание метода секущих:");
            solvingReport.AddParagraph(string.Format(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала,
Уравнение содержит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
X0 = a и X1 = a + {1} - начальные приближения метода секущих
Следующие точки приближения вычисляются по итерационной формуле:", '\u2264', '\u03B5'));
            solvingReport.AddImage(Properties.Resources.SecandMethodFormulaImage, 180f, 60f);
            solvingReport.AddParagraph(string.Format(@"Действие повторяется до тех пор, пока |Xi+1 - Xi| > {0}
где {0} - погрешность решения уравнения", '\u03B5'));
            solvingReport.AddPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания метода хорд
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddChordMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание метода хорд:");
            solvingReport.AddParagraph(string.Format(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала
Уравнение содержит одина корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
X0 = a и X2 = b - начальные приближения метода хорд
В качестве фиксированной точке выбирается та точка, в которой произведение значения функции на 2-ю производную больше 0
Следующие точки приближения вычисляются по итерационной формуле:", '\u2264'));
            solvingReport.AddImage(Properties.Resources.ChordMethodImage, 140f, 45f);
            solvingReport.AddParagraph(string.Format(@"После получения очередного приближения приближения выбиарается новый интевал с границами nextX и одна точка из lastX или fixedX,
значение функции в которой отличается знаком от знака значения функции в точке nextX
Действия продолжаются до тех пор, по длина интервала поиска корня больше заданной прогешности решения {0}", '\u03B5'));
            solvingReport.AddBoldPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания комбинированного метода хорд и касательных
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddCombinedMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание комбинированного метода хорд и касательных:");
            solvingReport.AddParagraph(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала
Уравнение содержит одина корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
Итерационные формулы комбинировнанного метода:");
            solvingReport.AddImage(Properties.Resources.CombinedMethidDiscription, 300f, 150f);
            solvingReport.AddParagraph(string.Format(@"Действие повторяется до тех пор, пока интервал поиска корня [a, b] больше заданной погрешности {0}", '\u03B5'));
            solvingReport.AddBoldPhrase("\n");
        }

        /// <summary>
        /// Добавление в отчет описания метода Риддера
        /// </summary>
        /// <param name="solvingReport"> Отчет </param>
        public static void AddRiddersMethodDiscription(Report solvingReport)
        {
            solvingReport.AddBoldParagraph("Описание метода Ридерса:");
            solvingReport.AddParagraph(string.Format(@"Пусть a - нижняя граница интервала, b - верхняя граница интервала
Уравнение содержит один корень на интервале [a, b], если выполняется условие f(a) * f(b) {0} 0
В качестве первых двух точек приближения берем границы интервала: X1 = a, X2 = b
Очередное приближение метода вычисляется по итерационной формуле:", '\u2264'));
            solvingReport.AddImage(Properties.Resources.RiddersMethodFormula, 170f, 35f);
            solvingReport.AddParagraph(string.Format(@"где {0} = (X[k-1] + X[k]) / 2
Функция sign(x) определена следующих образом:
   Если x > 0 => sign(x) = 1
   Если x = 0 => sign(x) = 0
   Если x < 0 => sign(x) = -1
Действие продолжается до тех пор, пока |x[k+1] - x[k]| > {1}, где {1} - заданная погрешность
В качетсве корня берется середина отрезка [x[k+1] - x[k]]", '\u03B2', '\u03B5'));
            solvingReport.AddPhrase("\n");
        }
    }
}