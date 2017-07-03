using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SolvingReportLibrary
{
    /// <summary>
    /// Отчет по численному решению уравнения
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Статический конструктор отчета решения уравнения
        /// </summary>
        static Report()
        {
            // Настройка шрифта
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new Font(baseFont, 8, Font.NORMAL);
            boldFont = new Font(baseFont, 8, Font.BOLD);
            tableFont = new Font(baseFont, 6, Font.NORMAL);
        }

        /// <summary>
        /// Шрифт записи файла
        /// </summary>
        static readonly Font font;

        /// <summary>
        /// Жирный шрифт записи файла
        /// </summary>
        static readonly Font boldFont;

        /// <summary>
        /// Шрифт текста таблицы
        /// </summary>
        static readonly Font tableFont;

        /// <summary>
        /// Конструктор отсчета
        /// </summary>
        public Report()
        {
            textElemntsList = new List<IElement>();
            table = null;
        }

        /// <summary>
        /// Список элементов текста
        /// </summary>
        List<IElement> textElemntsList;

        /// <summary>
        /// Метод добавления элемента текста
        /// </summary>
        /// <param name="message"></param>
        public void AddPhrase(string message)
        {
            Phrase phrase = new Phrase(message, font);
            textElemntsList.Add(phrase); 
        }

        /// <summary>
        /// Добавить жирный текст
        /// </summary>
        /// <param name="message"> Текст </param>
        public void AddBoldPhrase(string message)
        {
            Phrase phrase = new Phrase(message, boldFont);
            textElemntsList.Add(phrase);
        }

        /// <summary>
        /// Добавления параграфа в список элементов текста
        /// </summary>
        /// <param name="message"> Текст параграфа </param>
        public void AddParagraph(string message)
        {
            Paragraph paragraph = new Paragraph(message, font);
            textElemntsList.Add(paragraph);
        }

        /// <summary>
        /// Добавить жирный параграф в список элеметов теста
        /// </summary>
        /// <param name="message"> Текст параграфа </param>
        public void AddBoldParagraph(string message)
        {
            Paragraph paragraph = new Paragraph(message, boldFont);
            textElemntsList.Add(paragraph);
        }

        /// <summary>
        /// Запись файла результата
        /// </summary>
        public void WriteReport()
        {
            using (Document document = new Document())
            {
                using (FileStream stream = new FileStream("LastReport.pdf", FileMode.Create))
                {
                    document.SetPageSize(PageSize.A4);
                    document.SetMargins(20, 10, 10, 10);
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    foreach (IElement element in textElemntsList)
                    {
                        document.Add(element);
                    }
                    document.Close();
                }
                textElemntsList = null;
            }
        }

        /// <summary>
        /// Таблица для записи в файл
        /// </summary>
        PdfPTable table;
        
        /// <summary>
        /// Создание таблицы для записы в файл
        /// </summary>
        /// <param name="columnCount"> Число столбцов таблицы </param>
        public void CreateTable(int columnCount)
        {
            textElemntsList.Add(new Phrase("\n"));
            table = new PdfPTable(columnCount);
            table.WidthPercentage = 100;
        }

        /// <summary>
        /// Добавление значений в таблицу
        /// </summary>
        /// <param name="cells"> Значения ячеек </param>
        public void AddTableCells(params string[] cells)
        {
            if (table != null)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase(cells[i], tableFont)));
                }
            }
        }

        /// <summary>
        /// Завершение работы с таблицей
        /// </summary>
        public void CloseTable()
        {
            if (table != null)
            {
                textElemntsList.Add(table);
                textElemntsList.Add(new Phrase("\n"));
            }
            table = null;
        }

        /// <summary>
        /// Добавления изображения в отчет
        /// </summary>
        ///<param name="image"> Изображение </param>
        ///<param name="height"> Длина изображения</param>
        ///<param name="width">Высота изображения </param>
        public void AddImage(System.Drawing.Bitmap image, float height, float width)
        {
            Image img = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Bmp);
            img.ScaleAbsolute(height, width);
            textElemntsList.Add(img);
        }
    }
}
