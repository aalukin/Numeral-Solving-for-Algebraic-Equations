using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolishNotationLibrary;
using SolvingReportLibrary;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Решение уравнения
    /// </summary>
    public class Solving
    {
        /// <summary>
        /// Точность решения (количество знаков после запятой)
        /// </summary>
        int digitsCountAfterSeparator;

        /// <summary>
        /// Точность решения уравнения
        /// </summary>
        double epsilon;

        /// <summary>
        /// Левая граница интервала поиска корней
        /// </summary>
        double intervalMin;

        /// <summary>
        /// Получить левую границу интервала решения уравнения
        /// </summary>
        public double GetIntervalMin
        {
            get
            {
                return intervalMin;
            }
        }

        /// <summary>
        /// Правая граница интервала поиска корней
        /// </summary>
        double intervalMax;

        /// <summary>
        /// Получить парвую границу интервала решения уравнения
        /// </summary>
        public double GetInteravalMax
        {
            get
            {
                return intervalMax;
            }
        }

        /// <summary>
        /// Число подынтервалов
        /// </summary>
        uint subintervalsCount;

        /// <summary>
        /// Уравнение
        /// </summary>
        PostfixNotation equation;

        /// <summary>
        /// Получить уравнение
        /// </summary>
        public PostfixNotation GetEquation
        {
            get
            {
                return equation;
            }
        }

        /// <summary>
        /// Выбранный численный метод решения уравнения
        /// </summary>
        NumeralMethod selectedNumeralMethod;

        /// <summary>
        /// Список найденных корней
        /// </summary>
        List<Root> rootsList;

        /// <summary>
        /// Получить список найденных корней
        /// </summary>
        public List<Root> GetRootsList
        {
            get
            {
                return this.rootsList;
            }
        }

        /// <summary>
        /// Список точек, в которых сошелся метод, но не подходящие по усовию
        /// </summary>
        List<FakeRoot> fakeRootList;

        /// <summary>
        /// Получить список точек, в которых сошелся метод, но подходящих по условию
        /// </summary>
        public List<FakeRoot> GetFakeRootsList
        {
            get
            {
                return this.fakeRootList;
            }
        }

        /// <summary>
        /// Имя выбранного численного метода решения уравнения
        /// </summary>
        public string SelectedMethodName { get; private set; }

        /// <summary>
        /// Условие сходимости численного метода решения уравнения
        /// </summary>
        public string ConditionOfConvergance { get; private set; }

        /// <summary>
        /// Список разрывов функции
        /// </summary>
        List<DiscontinuityException> discontinuityList;

        /// <summary>
        /// Отчет численного решения файла
        /// </summary>
        Report solvingReport;

        /// <summary>
        /// Получить отчет численного решения файла
        /// </summary>
        public Report GetSolvingReport
        {
            get
            {
                return solvingReport;
            }
        }

        /// <summary>
        /// Конструктор решения
        /// </summary>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBorder"> Правая граница интервала </param>
        /// <param name="subintervalsCount"> Число подынтервалов </param>
        /// <param name="digitsCountAfterSeparator"> Точность (количество знаков после запятой) </param>
        /// <param name="selectedMethod"> Выбранный метод решения уравнения </param>
        public Solving(PostfixNotation equation, double intervalMin, double intervalMax, uint subintervalsCount, int digitsCountAfterSeparator, NumeralMethods selectedMethod)
        {
            solvingReport = new Report();
            this.equation = equation;
            this.intervalMin = intervalMin;
            this.intervalMax = intervalMax;
            this.subintervalsCount = subintervalsCount;
            this.digitsCountAfterSeparator = digitsCountAfterSeparator;
            double.TryParse("1E-" + this.digitsCountAfterSeparator, out this.epsilon);
            NumeralSolvingMethodSetup(selectedMethod);
            fakeRootList = new List<FakeRoot>();
            discontinuityList = new List<DiscontinuityException>();
            this.rootsList = SolveEquation();
        }

        /// <summary>
        /// Получения корней на интервале
        /// </summary>
        /// <returns> Список корней уравнения </returns>
        List<Root> SolveEquation()
        {
            List<Root> roots = new List<Root>();
            double delta = (this.intervalMax - this.intervalMin) / this.subintervalsCount;
            double leftBorber, rigthBorder;
            solvingReport.AddBoldParagraph("Решение:\n");
            solvingReport.AddParagraph(string.Format("Разобьем интервал [{0}; {1}] на {2} частей и на каждом подыинтервале применим выбранный метод решения уравнения.", intervalMin, intervalMax, subintervalsCount));
            solvingReport.AddPhrase("\n");
            for (uint i = 0; i < this.subintervalsCount; i++)
            {
                solvingReport.AddBoldParagraph("Интервал № " + (i + 1));
                leftBorber = this.intervalMin + i * delta;
                rigthBorder = leftBorber + delta;
                solvingReport.AddParagraph("Нижняя граница интервала a = " + Round(leftBorber));
                solvingReport.AddParagraph("Верхняя граница интервала b = " + Round(rigthBorder));
                try
                {
                    if (this.selectedNumeralMethod.ThisIntervalContainRoot(leftBorber, rigthBorder))
                    {
                        Root root = this.selectedNumeralMethod.GetRoot(leftBorber, rigthBorder);
                        solvingReport.CloseTable();
                        if (root is FakeRoot)
                        {
                            fakeRootList.Add((FakeRoot)root);
                        }
                        else
                        {
                            solvingReport.AddBoldPhrase("Ответ: ");
                            solvingReport.AddPhrase(string.Format("{0} = {1}, f({0}) = {2}\n\n", equation.GetVariableSymbol, root.GetRootValue, Round(equation.Calcuate(root.GetRootValue))));
                            if (roots.Count == 0 || root.GetRootValue != roots.Last().GetRootValue)
                            {
                                roots.Add(root);
                            }
                        }
                    }
                }
                catch (DiscontinuityException de)
                {
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(de.ToString());
                    solvingReport.AddPhrase("\n");
                    discontinuityList.Add(de);
                    continue;
                }
                catch (FirstDirevativeException)
                {
                    continue;
                }
                catch (MethodIsNotAplicableException ex)
                {
                    solvingReport.CloseTable();
                    solvingReport.AddParagraph(ex.ToString());
                    solvingReport.AddPhrase("\n");
                    continue;
                }
            }
            return roots;
        }

        /// <summary>
        /// Установка выбранного метода решения уравнения
        /// </summary>
        void NumeralSolvingMethodSetup(NumeralMethods selectedMethod)
        {
            switch (selectedMethod)
            {
                case NumeralMethods.BisectionMethod:
                    {
                        selectedNumeralMethod = new BisectionMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, solvingReport);
                        SelectedMethodName = "Метод половинного деления";
                        ConditionOfConvergance = string.Format("Функция f({0}) должна быть непрерывна и монотонна на интервале [{1}; {2}]", this.equation.GetVariableSymbol, Round(this.intervalMin), Round(this.intervalMax));
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddBisectionMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.GoldSectionMethod:
                    {
                        selectedNumeralMethod = new GoldenSectrionMethod(equation, epsilon, digitsCountAfterSeparator, solvingReport);
                        SelectedMethodName = "Метод Золотого сечения";
                        ConditionOfConvergance = string.Format("Функция f({0}) должна быть непрерывна и монотонна на интервале [{1}; {2}]", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax));
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddGoldenSectionMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.NewtonsMethod:
                    {
                        selectedNumeralMethod = new NewtonsMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, solvingReport);
                        this.SelectedMethodName = "Метод Ньютона (Метод касательных)";
                        this.ConditionOfConvergance = String.Format("Функции f({0}), f'({0}) и f\"({0}) долны быть непревыны на интервале [{1}; {2}]\r\nf'({0}) {3} 0\r\nf\"({0}) не меняет своего знака на интервале", this.equation.GetVariableSymbol, Round(this.intervalMin), Round(this.intervalMax), '\u2260');
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddNewtonsMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.ModifiedNewtonsMethod:
                    {
                        this.selectedNumeralMethod = new ModifiedNewtonsMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, this.solvingReport);
                        this.SelectedMethodName = "Модифицированный метод Ньютона";
                        this.ConditionOfConvergance = string.Format("Функции f({0}), f'({0}) и f\"({0}) должны быть непрерывны на интервале [{1}; {2}]\r\nf'({0}) {3} 0\r\nf\"({0}) не меняет своего знака на интервале", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax), '\u2260');
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddModifiedNewtonsMethidDiscriotion(solvingReport);
                        break;
                    }
                case NumeralMethods.SecantMethod:
                    {
                        this.selectedNumeralMethod = new SecantMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, solvingReport);
                        this.SelectedMethodName = "Метод секущих";
                        this.ConditionOfConvergance = string.Format("Функции f({0}), f'({0}) и f\"({0}) должны быть непрерывны на интервале [{1}; {2}]\r\nf'({0}) {3} 0\r\nf\"({0}) не меняет своего знака на интервале", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax), '\u2260');
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddSecandMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.ChordMethod:
                    {
                        this.selectedNumeralMethod = new ChordMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, solvingReport);
                        this.SelectedMethodName = "Метод хорд";
                        this.ConditionOfConvergance = string.Format("Функции f({0}), f'({0}) и f\"({0}) должны быть непрерывны на интервале [{1}; {2}]\r\nf'({0}) {3} 0\r\nf\"({0}) не меняет своего знака на интервале", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax), '\u2260');
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddChordMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.CombinedMethod:
                    {
                        selectedNumeralMethod = new CombinedMethod(equation, epsilon, digitsCountAfterSeparator, solvingReport);
                        SelectedMethodName = "Комбинированный метод хорд и касательных";
                        this.ConditionOfConvergance = string.Format("Функции f({0}), f'({0}) и f\"({0}) должны быть непрерывны на интервале [{1}; {2}]\r\nf'({0}) {3} 0\r\nf\"({0}) не меняет своего знака на интервале", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax), '\u2260');
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddCombinedMethodDiscription(solvingReport);
                        break;
                    }
                case NumeralMethods.RiddersMethod:
                    {
                        this.selectedNumeralMethod = new RiddersMethod(this.equation, this.epsilon, this.digitsCountAfterSeparator, this.solvingReport);
                        this.SelectedMethodName = "Метод Риддера";
                        this.ConditionOfConvergance = string.Format("Функция f({0}) должна быть непрерывна и монотонна на интервале [{1}; {2}]", equation.GetVariableSymbol, Round(intervalMin), Round(intervalMax));
                        AddRepotHeadler();
                        MethodsDisrtiptions.AddRiddersMethodDiscription(solvingReport);
                        break;
                    }
                default:
                    {
                        throw new Exception("Метод не существует");
                    }
            }
        }

        /// <summary>
        /// Добавление шапки отчета по решению уравнения
        /// </summary>
        void AddRepotHeadler()
        {
            solvingReport.AddBoldPhrase("Уравнение:\n");
            solvingReport.AddPhrase(equation.GetInfixNotation + " = 0\n");
            solvingReport.AddBoldPhrase("Выбранный метод решения уравнения: ");
            solvingReport.AddPhrase(SelectedMethodName + "\n");
            solvingReport.AddBoldParagraph("Условие сходимости метода:\n");
            solvingReport.AddParagraph(ConditionOfConvergance );
            solvingReport.AddBoldPhrase("Интервал решения: ");
            solvingReport.AddPhrase(string.Format("[{0}; {1}]\n", Round(intervalMin), Round(intervalMax)));
            solvingReport.AddBoldPhrase("Количество разбиений интервала: ");
            solvingReport.AddPhrase(subintervalsCount + "\n");
            solvingReport.AddBoldPhrase("Погрешность решения:" );
            solvingReport.AddPhrase(epsilon.ToString() + "\n\n");
        }

        /// <summary>
        /// Округление числа
        /// </summary>
        /// <param name="x"> Число для округления </param>
        /// <returns> Округленное число </returns>
        double Round(double x)
        {
            if (x >= 0)
            {
                return Math.Round(x + this.epsilon / 10, this.digitsCountAfterSeparator);
            }
            else
            {
                return Math.Round(x - this.epsilon / 10, this.digitsCountAfterSeparator);
            }
        }
    }
}
