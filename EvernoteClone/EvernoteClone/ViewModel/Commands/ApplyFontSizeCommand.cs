using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ApplyFontSizeCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is object[] values &&
                values[0] is RichTextBox rtb &&
                values[1] is double size)
            {
                TextSelection selection = rtb.Selection;
                if (!selection.IsEmpty)
                {
                    selection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
                }
                else
                {
                    rtb.FontSize = size;
                }
            }
        }
    }
}
