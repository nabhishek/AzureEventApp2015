using System.Collections.Generic;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    class PickerCell : ViewCell
    {
        public PickerCell(IEnumerable<FormFieldItem> items)
        {
            var picker = new Picker();
            foreach (var formFieldItem in items)
            {
                picker.Items.Add(formFieldItem.Text);
            }
            View = picker;
        }
    }
}