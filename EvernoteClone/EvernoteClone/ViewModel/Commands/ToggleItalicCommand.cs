using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ToggleItalicCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly NotesVM _vm;
        public ToggleItalicCommand(NotesVM vm) => _vm = vm;
        public bool CanExecute(object? parameter) => parameter is RichTextBox;
        public void Execute(object? parameter)
        {
            if (parameter is RichTextBox rtb)
                FormattingHelper.ToggleItalic(rtb);
        }
    }
}
