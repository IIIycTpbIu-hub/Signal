using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    public class ImageCheckBox : ICheckBoxContent
    {
        CheckBox checkBox;
        public ImageCheckBox(CheckBox checkBox)
        {
            this.checkBox = checkBox;
        }

        public string GetCheckBoxContent()
        {
            Grid checkBoxContent = checkBox.Content as Grid;
            UIElementCollection labelContent = checkBoxContent.Children;
            Label temp = new Label();
            foreach (var children in labelContent)
            {
                if (children is Label)
                {
                    temp = children as Label;
                    break;
                }
            }
            string content = temp.Content.ToString();
            return content;
        }
    }
}
