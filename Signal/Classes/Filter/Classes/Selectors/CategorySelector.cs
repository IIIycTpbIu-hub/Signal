using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Предоставляет инстументы для создания части строки фильтров по категории сигнала
    /// </summary>
    class CategorySelector : IQueryPartConstructor
    {
        List<CheckBox> selectedCheckBoxes;
        string categoryQuery = "";

        public CategorySelector(List<CheckBox> selectedCheckBoxes)
        {
            this.selectedCheckBoxes = selectedCheckBoxes;
        }
        /// <summary>
        /// формирует фильтр по категории сигнала
        /// </summary>
        /// <returns></returns>
        public string MakeQueryPart()
        {
            string queryPart = "";
            for (int i = 0; i < selectedCheckBoxes.Count; i++)
            {
                if (i != selectedCheckBoxes.Count - 1)
                    queryPart += "(Categories." + selectedCheckBoxes[i].Name + "= \"true\") OR";
                else queryPart += "(Categories." + selectedCheckBoxes[i].Name + "= \"true\") ";
            }
            if (queryPart != "")
            {
                categoryQuery = "AND (" + queryPart + ")";
                return categoryQuery;
            }
            else return "";
            
            
        }
    }
}
