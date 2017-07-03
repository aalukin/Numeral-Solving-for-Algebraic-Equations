using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема скобка
    /// </summary>
    public class Bracket : Token
    {
        /// <summary>
        /// Используемые скобки
        /// </summary>
        public static readonly string avaliableBrackets = "()";

        /// <summary>
        /// Типы скобок
        /// </summary>
        public enum BracketType
        {
            OpenBracket,
            CloseBracket
        }

        /// <summary>
        /// Тип скобки
        /// </summary>
        BracketType _bracketType;

        /// <summary>
        /// Свойства возвращает тип скобки
        /// </summary>
        public BracketType GetBracketType
        {
            get
            {
                return _bracketType;
            }
        }

        /// <summary>
        /// Конструктор лексемы типа "скобка"
        /// </summary>
        /// <param name="ch"> Символ скобки </param>
        public Bracket(char ch) : base()
        {
            _bracketType = FindBracketType(ch);
        }

        /// <summary>
        /// Определение типа скобки
        /// </summary>
        /// <param name="ch"> Изображение скобки </param>
        /// <returns> Тип скобки </returns>
        BracketType FindBracketType(char ch)
        {
            if (ch == '(')
            {
                return BracketType.OpenBracket;
            }
            else
            {
                return BracketType.CloseBracket;
            }
        }

        /// <summary>
        /// Получить приориет скобок
        /// </summary>
        public override int GetPriority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Получить строковое представление скобок
        /// </summary>
        public override string GetTokenString
        {
            get
            {
                if (_bracketType == BracketType.OpenBracket)
                {
                    return "(";
                }
                else
                {
                    return ")";
                }
            }
        }
    }
}
