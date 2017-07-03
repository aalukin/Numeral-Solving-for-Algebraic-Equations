using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumeralMethodsLibrary
{
    /// <summary>
    /// Точка, в который сошелся метод, но не подходящий по условию
    /// </summary>
    public class FakeRoot : Root
    {
        /// <summary>
        /// Точка, в который сошелся чиленный метод, но не являющийся корней
        /// </summary>
        /// <param name="pointValue"> Значение </param>
        /// <param name="leftBorder"> Левая граница интервала </param>
        /// <param name="rightBoredel"> Правая граница интервала </param>
        /// <param name="eqautionVariable"> Символ переменной</param>
        public FakeRoot(double pointValue, double leftBorder, double rightBorder, char eqautionVariable) : base (pointValue, leftBorder, rightBorder, eqautionVariable)
        {
            
        }
    }
}
