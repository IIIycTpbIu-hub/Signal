using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Предоставляет инстументы для создания части строки фильтров по режиму модуляции
    /// </summary>
    class ModulationSelector : SigTableQueryBuiler, IQueryPartConstructor
    {
        List<CheckBox> selectedCheckBoxes;
        string modulationQuery = "";

        public ModulationSelector(List<CheckBox> selectedCheckBoxes)
        {
            this.selectedCheckBoxes = selectedCheckBoxes;
        }
        /// <summary>
        /// формируте фильтр по режиму модуляции
        /// </summary>
        /// <returns></returns>
        public string MakeQueryPart()
        {
            string checkBoxContent = ""; ;
            checkBoxContent += base.MakeQueryString("SigTable.Модуляция", selectedCheckBoxes, this);
            if (checkBoxContent != "")
            {
                modulationQuery = "AND (" + checkBoxContent + ")";
                return modulationQuery;
            }
            else return "";
        }
    }
}
