using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    class LabelCheckBox : ICheckBoxContent
    {
        CheckBox checkBox;
        public LabelCheckBox(CheckBox checkBox)
        {
            this.checkBox = checkBox;
        }

        public string GetCheckBoxContent()
        {
            Label temp = checkBox.Content as Label;
            string content = temp.Content.ToString();
            return content;
        }
    }
}
