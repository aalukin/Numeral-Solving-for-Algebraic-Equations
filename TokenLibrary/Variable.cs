using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема переменная
    /// </summary>
    public class Variable : Token
    {
        /// <summary>
        /// Выбранная переменная
        /// </summary>
        static char _valiableSymbol;

        /// <summary>
        /// Получить символ переменной
        /// </summary>
        public static char GetVariableSymbol
        {
            get
            {
                return _valiableSymbol;
            }
        }

        /// <summary>
        /// Конструктор лексемы типа переменная
        /// </summary>
        /// <param name="ch"></param>
        public Variable(char ch) : base()
        {
            ch = char.ToUpper(ch);
            _valiableSymbol = ch;
        }

        /// <summary>
        /// Метод возвращает строковае представление символа переменной
        /// </summary>
        public override string GetTokenString
        {
            get
            {
                return _valiableSymbol.ToString();
            }
        }
    }
}
