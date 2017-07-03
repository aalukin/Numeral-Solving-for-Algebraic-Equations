using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NumeralMethodsLibrary;
using System.Text;
using System.Windows.Forms;

namespace MainForm
{
    /// <summary>
    /// Генератор вывода ответа
    /// </summary>
    class AnswerGenerator
    {
        /// <summary>
        /// Решение уравнения
        /// </summary>
        Solving sol;

        /// <summary>
        /// Текстовое окно с ответом
        /// </summary>
        RichTextBox answerRichTextBox;

        /// <summary>
        /// Получить форму ответа
        /// </summary>
        public RichTextBox GetAnswerRichTextBox
        {
            get
            {
                return this.answerRichTextBox;
            }
        }

        /// <summary>
        /// Заголовок вывода информации о выбранном методе решения
        /// </summary>
        static readonly string selectedMethodNameTitle = "Выбранный численный метод решения уравнения: ";

        /// <summary>
        /// Заголовок вывода информации о применимости метода решения
        /// </summary>
        static readonly string conditionOfConverganceTitle = "Условие сходимости метода: ";

        /// <summary>
        /// Заголовок вывода найденных корней уравнения
        /// </summary>
        static readonly string rootsTitle = "Найденные корни уравнения: ";

        /// <summary>
        /// Заголовок вывода точек, в которых сходился метод
        /// </summary>
        static readonly string fakeRootsTitle = "Точки, в которых сходился метод:";

        /// <summary>
        /// Конструктор генератора ответа
        /// </summary>
        /// <param name="sol"> Решение уравнения </param>
        public AnswerGenerator(Solving sol, RichTextBox answerRichTextBox)
        {
            this.sol = sol;
            this.answerRichTextBox = answerRichTextBox;
            this.answerRichTextBox.MaxLength = int.MaxValue;
            AnswerGeneration();
        }

        /// <summary>
        /// Генерация шапки ответа
        /// </summary>
        void AnswerGeneration()
        {
            this.answerRichTextBox.Text = String.Empty;
            this.answerRichTextBox.AppendText(selectedMethodNameTitle);
            this.answerRichTextBox.Select(0, selectedMethodNameTitle.Length);
            this.answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Bold);
            this.answerRichTextBox.AppendText(sol.SelectedMethodName);
            this.answerRichTextBox.Select(this.answerRichTextBox.Text.Length - sol.SelectedMethodName.Length, sol.SelectedMethodName.Length);
            this.answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Regular);
            this.answerRichTextBox.AppendText("\r\n");
            this.answerRichTextBox.AppendText(conditionOfConverganceTitle);
            this.answerRichTextBox.Select(this.answerRichTextBox.Text.Length - conditionOfConverganceTitle.Length, conditionOfConverganceTitle.Length);
            this.answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Bold);
            this.answerRichTextBox.AppendText(this.sol.ConditionOfConvergance);
            this.answerRichTextBox.Select(this.answerRichTextBox.Text.Length - this.sol.ConditionOfConvergance.Length, this.sol.ConditionOfConvergance.Length);
            this.answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Regular);
            this.answerRichTextBox.AppendText("\r\n");
            this.answerRichTextBox.AppendText(rootsTitle);
            this.answerRichTextBox.Select(this.answerRichTextBox.Text.Length - rootsTitle.Length, rootsTitle.Length);
            this.answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Bold);
            this.answerRichTextBox.AppendText("\r\n");
            if (this.sol.GetRootsList.Count != 0)
            {
                foreach (Root root in this.sol.GetRootsList)
                {
                    this.answerRichTextBox.AppendText(root.ToString() + "\r\n");
                }
            }
            else
            {
                this.answerRichTextBox.AppendText("Корни не найдены");
            }
            this.answerRichTextBox.AppendText("\r\n");
            if (sol.GetFakeRootsList.Count != 0)
            {
                this.answerRichTextBox.AppendText(fakeRootsTitle);
                this.answerRichTextBox.Select(this.answerRichTextBox.Text.Length - fakeRootsTitle.Length, fakeRootsTitle.Length);
                answerRichTextBox.SelectionFont = new Font(this.answerRichTextBox.Font, FontStyle.Bold);
                answerRichTextBox.AppendText("\r\n");
                foreach (FakeRoot fakeRoot in sol.GetFakeRootsList)
                {
                    this.answerRichTextBox.AppendText(fakeRoot.ToString() + "\r\n");
                }
            }
        }
    }
}