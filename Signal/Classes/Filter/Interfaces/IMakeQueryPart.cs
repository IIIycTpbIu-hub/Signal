using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Интерфейс для реализации паттерна стратегии
    /// </summary>
    interface IQueryPartConstructor
    {
        string MakeQueryPart();
    }
}
