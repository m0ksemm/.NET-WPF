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





        private Visibility isVisibile;

        public Visibility IsVisibile
        {
            get { return isVisibile; }
            set
            {
                isVisibile = value;
                OnPropertyChanged(nameof(IsVisibile));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public SaveNoteCommand SaveNoteCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            SaveNoteCommand = new SaveNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisibile = Visibility.Collapsed;

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
            IsVisibile = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsVisibile = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
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

        private void LoadNoteContent()
        {
            if (SelectedNote == null)
            {
                // немає вибраної нотатки → RichTextBox очищається
                //SelectedNoteContentRtf = string.Empty;
                return;
            }

            if (!string.IsNullOrEmpty(SelectedNote.Content) && File.Exists(SelectedNote.Content))
            {
                try
                {
                    using var fs = new FileStream(SelectedNote.Content, FileMode.Open, FileAccess.Read);
                    using var reader = new StreamReader(fs);
                    //SelectedNoteContentRtf = reader.ReadToEnd(); // сюди кладемо вміст RTF-файлу
                }
                catch
                {
                   // SelectedNoteContentRtf = string.Empty; // fallback
                }
            }
            else
            {
                // якщо ще немає вмісту → RichTextBox чистий
               // SelectedNoteContentRtf = string.Empty;
            }
        }
    }
}
