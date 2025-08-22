using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ToggleBoldCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly NotesVM _vm;

        public ToggleBoldCommand(NotesVM vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object? parameter)
        {
            // parameter очікується RichTextBox
            return parameter is RichTextBox;
        }

        public void Execute(object? parameter)
        {
            if (parameter is RichTextBox rtb)
            {
                FormattingHelper.ApplyBold(rtb);
            }
        }
    }
}
