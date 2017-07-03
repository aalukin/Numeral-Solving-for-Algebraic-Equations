using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenLibrary
{
    /// <summary>
    /// Лексема
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        /// Получить символьное представление лексемы
        /// </summary>
        public virtual string GetTokenString
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Получить приоритет лексемы
        /// </summary>
        public virtual int GetPriority
        {
            get
            {
                return -1;
            }
        }
    }
}
