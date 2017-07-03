using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    /// <summary>
    /// Исключение, возникающее при некорректных значениях полей ввода гравного окна
    /// </summary>
    class InputException : Exception
    {
        /// <summary>
        /// Форма текстового ввода с ошибкой
        /// </summary>
        RichTextBox richTextBoxWithError;

        /// <summary>
        /// Свойство возвращает форму текстового ввода с ошибкой
        /// </summary>
        public RichTextBox GetRichTextBoxWithError
        {
            get
            {
                return this.richTextBoxWithError;
            }
        }

        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"> Сообщение об ошибке </param>
        /// <param name="textBoxs"> Форма ввода текста с ошибкой </param>
        public InputException(string message, RichTextBox textBoxs) : base(message)
        {
            this.richTextBoxWithError = textBoxs;
        }
    }
}
