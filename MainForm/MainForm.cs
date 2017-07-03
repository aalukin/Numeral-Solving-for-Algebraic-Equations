using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using NumeralMethodsLibrary;
using PolishNotationLibrary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SolvingReportLibrary;
using AcroPDFLib;
using ZedGraph;

namespace MainForm
{
    /// <summary>
    /// Главное окно программы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            methodsComboBox.SelectedIndex = 0;
            wasSyntaxException = false;
            intervalMinRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            intervalMaxRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            subintervalsCountRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            accuracyRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            inputTextBox.Select();
            StartFormView();
            answerRichTextBox.ReadOnly = true;
            answerRichTextBox.BackColor = Color.White;
            backgroundSolver = new AbortedBackgroundWorker();
            backgroundSolver.DoWork += new DoWorkEventHandler(backgroundSolver_DoWork);
            backgroundSolver.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundSolver_RunWorkerCompleted);
        }

        /// <summary>
        /// Начальный вид формы
        /// </summary>
        private void StartFormView()
        {
            startPictireBox.Show();
            loadPictureBox.Hide();
            loadLabel.Hide();
            outputTabControl.Hide();
            printToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Уравнение в инверсной записи
        /// </summary>
        PostfixNotation equation;

        /// <summary>
        /// Поток решения
        /// </summary>
        AbortedBackgroundWorker backgroundSolver;

        /// <summary>
        /// Метка отменнного решения
        /// </summary>
        bool isCanceled;

        /// <summary>
        /// Нажатие на кнопку "Решить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveButton_Click(object sender, EventArgs e)
        {
            try
            {
                equation = new PostfixNotation(inputTextBox.Text);
                MinMaxIntervalChecking();
                SunintervalCountInputChecking();
                AccuracyInputChecking();
                selectedMethod = (NumeralMethods)this.methodsComboBox.SelectedIndex;
                SolvingStartControlsEnabledChange();
                isCanceled = false;
                if (!backgroundSolver.IsBusy)
                {
                    backgroundSolver.RunWorkerAsync();
                }
            }
            catch (SyntaxException sx)
            {
                MessageBox.Show(sx.Message, "Cинтаксическая ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (sx.GetFirstPosition != -1)
                {
                    inputTextBox.Select(sx.GetFirstPosition, sx.GetSecondPosition - sx.GetFirstPosition);
                    inputTextBox.SelectionFont = new Font(inputTextBox.SelectionFont, FontStyle.Underline);
                    inputTextBox.SelectionColor = Color.Red;
                    wasSyntaxException = true;
                }
            }
            catch (InputException ix)
            {
                MessageBox.Show(ix.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ix.GetRichTextBoxWithError.Font = new Font(ix.GetRichTextBoxWithError.Font, FontStyle.Underline);
                ix.GetRichTextBoxWithError.ForeColor = Color.Red;
                wasInputException = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Событие изменения текста формы ввода уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (wasSyntaxException)
            {
                int coursorPosition = inputTextBox.SelectionStart;
                inputTextBox.SelectAll();
                inputTextBox.SelectionFont = new Font(inputTextBox.Font, FontStyle.Regular);
                inputTextBox.SelectionColor = Color.Black;
                wasSyntaxException = false;
                inputTextBox.Select(coursorPosition, 0);
            }
        }

        /// <summary>
        /// Метка о генерации исключения синтаксической ошибки при последней попытки решения уравнения
        /// </summary>
        bool wasSyntaxException;

        /// <summary>
        /// Нижняя граница интервала для решения уравения
        /// </summary>
        double intervalMin;

        /// <summary>
        /// Верхняя граница интервала для решения уравнения
        /// </summary>
        double intervalMax;

        /// <summary>
        /// Решение уравнение
        /// </summary>
        Solving solving;

        /// <summary>
        /// Выбранный численный метод решения уравнения
        /// </summary>
        NumeralMethods selectedMethod;

        /// <summary>
        /// Проверка ввода нижего и верхнего интевала решния уравнения
        /// </summary>
        void MinMaxIntervalChecking()
        {
            if (!double.TryParse(intervalMinRichTextBox.Text, out this.intervalMin))
            {
                throw new InputException("Некорректоое значения в окне ввода нижней границы интервала решения\r\nНеобходимо ввести вещественное число", intervalMinRichTextBox);
            }
            if (!double.TryParse(intervalMaxRichTextBox.Text, out this.intervalMax))
            {
                throw new InputException("Некорректное значение в окно ввода верхней границы интервала решения\r\nНеобходимо ввести вещественное числоа", intervalMaxRichTextBox);
            }
            double min = intervalMin;
            double max = intervalMax;
            intervalMin = Math.Min(min, max);
            intervalMax = Math.Max(min, max);
            if (Math.Abs(max - min) == 0)
            {
                throw new InputException("Длина интервала поиска корней не может быть равна 0", intervalMinRichTextBox);
            }
            if (Math.Abs(max - min) > 1000)
            {
                throw new InputException("Длина интервала поиска корней не может быть больше 1000", intervalMaxRichTextBox);
            }
            intervalMinRichTextBox.Text = intervalMin.ToString();
            intervalMinRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            intervalMaxRichTextBox.Text = intervalMax.ToString();
            intervalMaxRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        /// <summary>
        /// Метка генерации исключения ввода
        /// </summary>
        bool wasInputException;

        /// <summary>
        /// Изменение текста уравнения после возниковения исключения ввода
        /// </summary>
        /// <param name="rtb"></param>
        void InputRichTextBoxsTextChanged(RichTextBox rtb)
        {
            if (rtb.ForeColor == Color.Red)
            {
                rtb.Font = new Font(rtb.Font, FontStyle.Regular);
                rtb.ForeColor = Color.Black;
                wasInputException = false;
            }
        }

        /// <summary>
        /// Событие изменения текста формы ввода нижней границы интвервала решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalMinRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (wasInputException)
            {
                InputRichTextBoxsTextChanged(this.intervalMinRichTextBox);
            }
        }

        /// <summary>
        /// События изменения текста формы ввода верхней границы интервала решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalMaxRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (wasInputException)
            {
                InputRichTextBoxsTextChanged(this.intervalMaxRichTextBox);
            }
        }

        /// <summary>
        /// Проверка ввода количество подынтервалов решения уравнения
        /// </summary>
        void SunintervalCountInputChecking()
        {
            if (!uint.TryParse(subintervalsCountRichTextBox.Text, out subintervalsCount) || subintervalsCount == 0 || subintervalsCount > 1000)
            {
                throw new InputException("Некорректное значения поля ввода количества подынтервалов для решения уравнения\r\nПожалуйста, введите натуральное число, не превышающее 1000 и повторите попытку", this.subintervalsCountRichTextBox);
            }
        }

        /// <summary>
        /// Количество подынтервалов для решения уравнения
        /// </summary>
        uint subintervalsCount;

        /// <summary>
        /// Событие изменения текста формы ввода количества подынтервалов для решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subintervalsCountRichTextBox_TextChanged(object sender, EventArgs e)
        {
            this.subintervalsCountRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            if (wasInputException)
            {
                InputRichTextBoxsTextChanged(this.subintervalsCountRichTextBox);
            }
        }

        /// <summary>
        /// Проверка ввода точности решения уравнения
        /// </summary>
        void AccuracyInputChecking()
        {
            if (!int.TryParse(this.accuracyRichTextBox.Text, out this.countDigitsAfterSeparator) || countDigitsAfterSeparator <= 0 || countDigitsAfterSeparator > 14)
            {
                throw new InputException("Некорректное значение поля ввода точности решения уравнения\r\nВ поле необходимо ввести целое число в пределах от 1 до 14, соответвующее количеству знаков после запятой точности решения\r\nПожалуйста, исправьте ошибку и повторите попытку", this.accuracyRichTextBox);
            }
        }

        /// <summary>
        /// Точность решения (количество знаков после запятой)
        /// </summary>
        int countDigitsAfterSeparator;

        /// <summary>
        /// Событие изменения текста формы ввода точности решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accuracyRichTextBox_TextChanged(object sender, EventArgs e)
        {
            accuracyRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            if (wasInputException)
            {
                InputRichTextBoxsTextChanged(this.accuracyRichTextBox);
            }
        }

        /// <summary>
        /// Вывод ответа на экран
        /// </summary>
        void ShowAnswer()
        {
            AnswerGenerator ag = new AnswerGenerator(this.solving, this.answerRichTextBox);
            this.answerRichTextBox = ag.GetAnswerRichTextBox;
        }

        /// <summary>
        /// Поток решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundSolver_DoWork(object sender, DoWorkEventArgs e)
        {
            solving = new Solving(equation, intervalMin, intervalMax, subintervalsCount, countDigitsAfterSeparator, selectedMethod);
            Action ChangeLoadLabel = new Action(() => 
            {
                ChangeLoadMessage("Запись отчета решения уравнения...");
            });
            if (loadLabel.InvokeRequired)
            {
                loadLabel.Invoke(ChangeLoadLabel);
            }
            else
            {
                ChangeLoadLabel();
            }
            solving.GetSolvingReport.WriteReport();
            ChangeLoadLabel = new Action(() =>
            {
                ChangeLoadMessage("Вывод отчета решения уравнения...");
            });
            if (loadLabel.InvokeRequired)
            {
                loadLabel.Invoke(ChangeLoadLabel);
            }
            else
            {
                ChangeLoadLabel();
            }
            Action ShowReport = new Action(() =>
            {
                var acro = (IAcroAXDocShim)pdfReader.GetOcx();
                acro.LoadFile("LastReport.pdf");
            });
            if (pdfReader.InvokeRequired)
            {
                pdfReader.Invoke(ShowReport);
            }
            else
            {
                ShowReport();
            }
            ChangeLoadLabel = new Action(() =>
            {
                ChangeLoadMessage("Вывод графика...");
            });
            if (loadLabel.InvokeRequired)
            {
                loadLabel.Invoke(ChangeLoadLabel);
            }
            else
            {
                ChangeLoadLabel();
            }
            Action ShowGraph = new Action(() =>
            {
                GraphCreating.DrawGraph(equationGraph, solving);
            });
            if (equationGraph.InvokeRequired)
            {
                equationGraph.Invoke(ShowGraph);
            }
            else
            {
                ShowGraph();
            }
            ChangeLoadLabel = new Action(() =>
            {
                ChangeLoadMessage("Завершение вывода ответа...");
            });
            if (loadLabel.InvokeRequired)
            {
                loadLabel.Invoke(ChangeLoadLabel);
            }
            else
            {
                ChangeLoadLabel();
            }
            Action ShowAnswer = new Action(() =>
            {
                this.ShowAnswer();
            });
            if (answerRichTextBox.InvokeRequired)
            {
                answerRichTextBox.Invoke(ShowAnswer);
            }
            else
            {
                ShowAnswer();
            }
        }

        /// <summary>
        /// Завершение потока решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundSolver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                SolvingCompleteControlsEnabledChange();
        }

        /// <summary>
        /// Изменение размера размера изображения загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadPictureBox_SizeChanged(object sender, EventArgs e)
        {
            loadLabel.Location = new Point(loadPictureBox.Location.X + (loadPictureBox.Width / 2) - (loadLabel.Width / 2), loadLabel.Location.Y);
        }

        /// <summary>
        /// Настройка активности элементов формы при старте решения уравнения
        /// </summary>
        private void SolvingStartControlsEnabledChange()
        {
            foreach (Control control in inputGroupBox.Controls)
            {
                if (control == cancelButton || control is LinkLabel)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
            saveToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Enabled = false;
            outputTabControl.Hide();
            startPictireBox.Hide();
            loadPictureBox.Show();
            ChangeLoadMessage("Поиск корней уравнения...");
            loadLabel.Show();
        }

        /// <summary>
        /// Настройка активности элементов формы при завершении рещения уравнения
        /// </summary>
        private void SolvingCompleteControlsEnabledChange()
        {
            foreach (Control control in inputGroupBox.Controls)
            {
                if (control == cancelButton)
                {
                    control.Enabled = false;
                }
                else
                {
                    control.Enabled = true;
                }
            }
            if (isCanceled)
            {
                StartFormView();
            }
            else
            {
                outputTabControl.Show();
                loadLabel.Hide();
                loadPictureBox.Hide();
                saveToolStripMenuItem.Enabled = true;
                printToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Изменение теста информации состояни программы
        /// </summary>
        /// <param name="message"></param>
        private void ChangeLoadMessage(string message)
        {
            loadLabel.Text = message;
            loadLabel.Location = new Point(loadPictureBox.Location.X + (loadPictureBox.Width / 2) - (loadLabel.Width / 2), loadLabel.Location.Y);
        }

        /// <summary>
        /// Отмена решения уравнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (backgroundSolver.IsBusy)
            {
                isCanceled = true;
                backgroundSolver.Abort();
                backgroundSolver.Dispose();
            }
        }

        /// <summary>
        /// Нажатие на кнопку меню "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramBox aboutTheProgramBox = new AboutProgramBox();
            aboutTheProgramBox.ShowDialog();
        }

        /// <summary>
        /// Нажатие на кнопку "Сохранить отчет"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Adobe pdf (*.pdf)|*.pdf";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(@"LastReport.pdf", saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Печать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var printDialog = (IAcroAXDocShim)pdfReader.GetOcx();
                printDialog.printWithDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// События нажатия кнопки "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramBox aboutTheProgram = new AboutProgramBox();
            aboutTheProgram.ShowDialog();
        }

        /// <summary>
        /// Событие нажатия на кнопку "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Событие нажатия на кнопку "Справка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Событие нажатия на сслыку ввода интервала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm", HelpNavigator.TopicId, "5");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Событие нажания на ссылку ввода количества разбиений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subIntervalsCountlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm", HelpNavigator.TopicId, "6");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Нажатие на ссылку ввода точности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accuracyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm", HelpNavigator.TopicId, "7");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Нажатие на ссылку численных методов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mathodsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm", HelpNavigator.TopicId, (9 + methodsComboBox.SelectedIndex).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Нажатие на ссылку функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help.chm", HelpNavigator.TopicId, "4");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
