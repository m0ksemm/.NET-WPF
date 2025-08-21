using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class SaveNoteCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public NotesVM NotesVM { get; set; }
        public SaveNoteCommand(NotesVM vm)
        {
            NotesVM = vm;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            NotesVM.SaveNote();
        }
    }
}
