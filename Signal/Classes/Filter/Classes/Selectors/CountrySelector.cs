using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Предоставляет инстументы для создания части строки фильтров по принадлежности сигнала
    /// </summary>
    class CountrySelector : SigTableQueryBuiler, IQueryPartConstructor
    {
        
        List<CheckBox> selectedCheckBoxes;
        string countryQuery = "";

        public CountrySelector(List<CheckBox> selectedCheckBoxes)
        {
            this.selectedCheckBoxes = selectedCheckBoxes;
        }
        /// <summary>
        /// формирут фильтр по странам
        /// </summary>
        /// <returns></returns>
        public string MakeQueryPart()
        {
            string checkBoxContent = ""; ;
            checkBoxContent += base.MakeQueryString("SigTable.Принадлежность", selectedCheckBoxes, this);
            if (checkBoxContent != "")
            {
                countryQuery = "AND (" + checkBoxContent + ")";
                return countryQuery;
            }
            else return "";
        }
    }
}
