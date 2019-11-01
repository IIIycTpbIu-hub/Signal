using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    public abstract class SigTableQueryBuiler
    {
        public string MakeQueryString(string columnHead, List<CheckBox> ChosenCheckBoxes, SigTableQueryBuiler type)
        {
            string checkBoxContent;
            string queryPart = "";
            ICheckBoxContent content = null;
            for (int i = 0; i < ChosenCheckBoxes.Count; i++)
            {
                if (type is CountrySelector)
                {
                    content = new ImageCheckBox(ChosenCheckBoxes[i]);
                } else
                if (type is ModulationSelector || type is DemodulationSelector)
                {
                    content = new LabelCheckBox(ChosenCheckBoxes[i]);
                }
                checkBoxContent = content.GetCheckBoxContent();
                if (i != ChosenCheckBoxes.Count - 1)
                    queryPart += "(" + columnHead + " = \"" + checkBoxContent + "\") OR ";
                else queryPart += "(" + columnHead + " = \"" + checkBoxContent + "\") ";
            }
            return queryPart;
        }
    }
}
