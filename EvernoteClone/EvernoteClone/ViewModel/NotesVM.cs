using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        private Note selectedNote;
        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                if (selectedNote != value)
                {
                    selectedNote = value;
                    OnPropertyChanged(nameof(SelectedNote));

                    SelectedNoteChanged?.Invoke(this, EventArgs.Empty);

                    // одразу підтягуємо вміст

                }
            }
        }





        private Visibility isNotebookEditorVisibile;

        public Visibility IsNotebookEditorVisibile
        {
            get { return isNotebookEditorVisibile; }
            set
            {
                isNotebookEditorVisibile = value;
                OnPropertyChanged(nameof(IsNotebookEditorVisibile));
            }
        }

        private Visibility isNoteEditorVisibile;

        public Visibility IsNoteEditorVisibile
        {
            get { return isNoteEditorVisibile; }
            set
            {
                isNoteEditorVisibile = value;
                OnPropertyChanged(nameof(IsNoteEditorVisibile));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public SaveNoteCommand SaveNoteCommand { get; set; }
        public EditNoteTitleCommand EditNoteTitleCommand { get; set; }
        public EndEditingNoteTitleCommand EndEditingNoteTitleCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler SelectedNoteChanged;


        public ToggleBoldCommand ToggleBoldCommand { get; set; }
        public ToggleItalicCommand ToggleItalicCommand { get; set; }
        public ToggleUnderlineCommand ToggleUnderlineCommand { get; set; }
        public ApplyFontFamilyCommand ApplyFontFamilyCommand { get; set; }
        public ApplyFontSizeCommand ApplyFontSizeCommand { get; set; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            SaveNoteCommand = new SaveNoteCommand(this);

            EditNoteTitleCommand = new EditNoteTitleCommand(this);
            EndEditingNoteTitleCommand = new EndEditingNoteTitleCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsNotebookEditorVisibile = Visibility.Collapsed;
            IsNoteEditorVisibile = Visibility.Collapsed;




            ToggleBoldCommand = new ToggleBoldCommand(this);
            ToggleItalicCommand = new ToggleItalicCommand(this);
            ToggleUnderlineCommand = new ToggleUnderlineCommand(this);
            ApplyFontFamilyCommand = new ApplyFontFamilyCommand();
            ApplyFontSizeCommand = new ApplyFontSizeCommand();


            //GetNotebooks();
        }



        public async void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New notebook",
                UserId = App.UserID
            };

            await DatabaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public async void CreateNote(string notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            };

            await DatabaseHelper.Insert(newNote);

            GetNotes();
        }

        public async void GetNotebooks()
        {
            IEnumerable<Notebook>? notebooks = (await DatabaseHelper.Read<Notebook>());

            if (notebooks == null)
                return;

            IEnumerable<Notebook>? notebooksSorted = notebooks.Where(n => n.UserId == App.UserID);

            Notebooks.Clear();
            foreach (var notebook in notebooksSorted)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                List<Note>? notes = (await DatabaseHelper.Read<Note>());
                if (notes == null) return;
                List<Note>? notesSorted = notes.Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                Notes.Clear();

                SelectedNote = null;

                foreach (var note in notesSorted)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            IsNotebookEditorVisibile = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsNotebookEditorVisibile = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public void EditNoteTitle()
        {
            IsNoteEditorVisibile = Visibility.Visible;
        }

        public void StopEditingNoteTitle(Note note)
        {
            IsNoteEditorVisibile = Visibility.Collapsed;
            DatabaseHelper.Update(note);
            GetNotebooks();
        }

        public void SaveNote() 
        {
            if (SelectedNote == null) return;

            try
            {
                DatabaseHelper.Update(SelectedNote);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving note: {ex.Message}");
            }
        }

   




    }
}
