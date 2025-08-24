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
    public class ApplyFontFamilyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly NotesVM _vm;
        public ApplyFontFamilyCommand(NotesVM vm) => _vm = vm;
        public bool CanExecute(object? parameter) => parameter is object[];
        public void Execute(object? parameter)
        {
            if (parameter is object[] arr && arr.Length == 2 && arr[0] is RichTextBox rtb && arr[1] is string family)
                FormattingHelper.ApplyFontFamily(rtb, family);
        }
    }
}
