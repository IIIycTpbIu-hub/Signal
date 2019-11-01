using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Предоставляет инстументы для создания части строки фильтров по демодуляции
    /// </summary>
    class DemodulationSelector : SigTableQueryBuiler, IQueryPartConstructor
    {
        List<CheckBox> selectedCheckBoxes;
        string demodulationQuery = "";

        public DemodulationSelector(List<CheckBox> selectedCheckBoxes)
        {
            this.selectedCheckBoxes = selectedCheckBoxes;
        }
        /// <summary>
        /// формирует фильтр по демодуляции
        /// </summary>
        /// <returns></returns>
        public string MakeQueryPart()
        {
            string checkBoxContent = ""; ;
            checkBoxContent += base.MakeQueryString("SigTable.Режим_модуляции", selectedCheckBoxes, this);
            if (checkBoxContent != "")
            {
                demodulationQuery = "AND (" + checkBoxContent + ")";
                return demodulationQuery;
            }
            else return "";
        }
    }
}
