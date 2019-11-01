using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Предоставляет инстументы для создания части строки фильтров по диапазону частот
    /// </summary>
    class DiapazonSelector : IQueryPartConstructor
    {
        List<CheckBox> selectedCheckBoxes;
        string diapazonQuery = "";
        public DiapazonSelector(List<CheckBox> selectedCheckBoxes)
        {
            this.selectedCheckBoxes = selectedCheckBoxes;
        }
        /// <summary>
        /// формирует фильтр по диапазону частот
        /// </summary>
        /// <returns></returns>
        public string MakeQueryPart()
        {
            string queryPart = "";
            for (int i = 0; i < selectedCheckBoxes.Count; i++)
            {
                switch (selectedCheckBoxes[i].Name)
                {
                    case "MF":
                        {
                            if (i != selectedCheckBoxes.Count - 1)
                                queryPart += "((F1 BETWEEN 300000 and 3000000) OR (F2 BETWEEN 300000 and 3000000)) OR ";
                            else queryPart += "((F1 BETWEEN 300000 and 3000000) OR (F2 BETWEEN 300000 and 3000000)) ";
                        }
                        break;
                    case "HF":
                        {
                            if (i != selectedCheckBoxes.Count - 1)
                                queryPart += "((F1 between 3000000 and 29999999) or (F2 between 3000001 and 30000000)) OR ";
                            else queryPart += "((F1 between 3000000 and 29999999) or (F2 between 3000001 and 30000000)) ";
                        }
                        break;
                    case "VHF":
                        {
                            if (i != selectedCheckBoxes.Count - 1)
                                queryPart += "((F1 between 30000000 and 299999999) or (F2 between 30000001 and 300000000)) OR ";
                            else queryPart += "((F1 between 30000000 and 299999999) or (F2 between 30000001 and 300000000)) ";
                        }
                        break;
                    case "UHF":
                        {
                            if (i != selectedCheckBoxes.Count - 1)
                                queryPart += "((F1 between 300000000 and 2999999999) or (F2 between 300000001 and 3000000000)) OR ";
                            else queryPart += "((F1 between 300000000 and 2999999999) or (F2 between 300000001 and 3000000000)) ";
                        }
                        break;
                    case "SHF":
                        {
                            if (i != selectedCheckBoxes.Count - 1)
                                queryPart += "((F1 between 3000000000 and 29999999999) or (F2 between 3000000001 and 30000000000)) OR ";
                            else queryPart += "((F1 between 3000000000 and 29999999999) or (F2 between 3000000001 and 30000000000)) ";
                        }
                        break;
                }
            }
            if (queryPart != "")
            {
                diapazonQuery = "AND (" + queryPart + ")";
                return diapazonQuery;
            }
            else return "";
        }
    }
}
