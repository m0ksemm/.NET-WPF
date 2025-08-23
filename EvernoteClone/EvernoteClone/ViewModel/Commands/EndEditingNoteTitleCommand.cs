using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditingNoteTitleCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public NotesVM NotesVM { get; set; }
        public EndEditingNoteTitleCommand(NotesVM vm)
        {
            NotesVM = vm;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Note note = parameter as Note;
            if (note != null)
            {
                NotesVM.StopEditingNoteTitle(note);
            }
        }
    }
}
