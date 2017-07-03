using System;

namespace PolishNotationLibrary
{
    /// <summary>
    /// Синтаксическая ошибка в введеном пользователем выражении
    /// </summary>
    public class SyntaxException : Exception
    {
        /// <summary>
        /// Левая граница интервала подстроки с синтаксической ошибкой
        /// </summary>
        int firstPosition;

        /// <summary>
        /// Получить левую границу подстроки с ошибкой
        /// </summary>
        public int GetFirstPosition
        {
            get
            {
                return this.firstPosition;
            }
        }

        /// <summary>
        /// Правая гранциа интервала подстроки с синтаксической ошибкой
        /// </summary>
        int secondPosition;

        /// <summary>
        /// Получить правую границу подстроки с ошибкой
        /// </summary>
        public int GetSecondPosition
        {
            get
            {
                return this.secondPosition;
            }
        }

        /// <summary>
        /// Конструтор синтаксического исключения с сообщением об ошибке
        /// </summary>
        /// <param name="message"> Сообщение об ошибке </param>
        public SyntaxException(string message) : base(message)
        {
            this.firstPosition = -1;
            this.secondPosition = -1;
        }

        /// <summary>
        /// Конструктор синтаксического исключения с сообщением об ошибке и границами подстроки с ошибкой
        /// </summary>
        /// <param name="message"> Сообщение об ошибке </param>
        /// <param name="firstPosition"> Левая граница подстроки с ошибкой </param>
        /// <param name="secondPosition"> Правая граница подстроки с ошибкой </param>
        public SyntaxException(string message, int firstPosition, int secondPosition) : base(message)
        {
            this.firstPosition = firstPosition;
            this.secondPosition = secondPosition;
        }
    }
}
