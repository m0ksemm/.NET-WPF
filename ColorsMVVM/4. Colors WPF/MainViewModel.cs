using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _4._Colors_WPF
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ColorViewModel> ColorList { get; set; }
        public MainViewModel() 
        {
            ColorList = new ObservableCollection<ColorViewModel>();
        }
        public bool CheckAlpha { get; set; }
        public bool CheckRed { get; set; }
        public bool CheckGreen { get; set; }
        public bool CheckBlue { get; set; }

        private int colorAlpha = 0;
        public int ColorAlpha 
        {
            get { return colorAlpha; }
            set
            {
                colorAlpha = value;
                SetColor();
                OnPropertyChanged(nameof(colorAlpha));  
            }
        }

        private int colorRed = 0;
        public int ColorRed
        {
            get { return colorRed; }
            set
            {
                colorRed = value;
                SetColor();
                OnPropertyChanged(nameof(colorRed));
            }
        }

        private int colorGreen = 0;
        public int ColorGreen 
        {
            get { return colorGreen; }
            set
            {
                colorGreen = value;
                SetColor();
                OnPropertyChanged(nameof(colorGreen));
            }
        }
        private int colorBlue = 0;
        public int ColorBlue
        {
            get { return colorBlue; }
            set
            {
                colorBlue = value;
                SetColor();
                OnPropertyChanged(nameof(colorBlue));
            }
        }

        private string color = "#0";
        public string Color 
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged(nameof(color));
            }
        }

        private void SetColor() 
        {
            string textAlpha = Convert.ToString(colorAlpha, 16);
            if (textAlpha.Length == 1)
                textAlpha = "0" + textAlpha;
            if (!CheckAlpha)
                textAlpha = "00";
            //---------------------
            string textRed = Convert.ToString(colorRed, 16);
            if (textRed.Length == 1) 
                textRed = "0" + textRed;
            if (!CheckRed) 
                textRed = "00";
            //---------------------
            string textGreen = Convert.ToString(colorGreen, 16);
            if (textGreen.Length == 1)
                textGreen = "0" + textGreen;
            if (!CheckGreen)
                textGreen = "00";
            //---------------------
            string textBlue = Convert.ToString(colorBlue, 16);
            if (textBlue.Length == 1) 
                textBlue = "0" + textBlue;
            if (!CheckBlue) 
                textBlue = "00";
            //---------------------
            Color = "#" + textAlpha + textRed + textGreen + textBlue;
        }

        private int index_selected_listbox;
        public int Index_selected_listbox 
        {   
            get 
            {
                return index_selected_listbox;
            }
            set 
            {
                index_selected_listbox = value;
                OnPropertyChanged(nameof(index_selected_listbox));
            }
        }

        private DelegateCommand _AddCommand;
        public ICommand CommandAdd
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new DelegateCommand(Add, CanAdd);
                }
                return _AddCommand;
            }
        }

        private void Add(object o) 
        {
            ColorList.Add(new ColorViewModel(new _Colors_WPF.Color(color)));
        }

        private bool CanAdd(object o)
        {
            foreach (ColorViewModel cl in ColorList) 
            {
                if (cl.Name == color) 
                {
                    return false;
                }
            }
            return true;
        }

        private DelegateCommand _DeleteCommand;
        public ICommand CommandDelete
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new DelegateCommand(Delete, CanDelete);
                }
                return _DeleteCommand;
            }
        }
        private void Delete(object o)
        {
            ColorList.RemoveAt(Index_selected_listbox);
            if (ColorList.Count == 0)
                Index_selected_listbox = 0;
            Index_selected_listbox = -1;
        }
        private bool CanDelete(object o)
        {
            return true;
        }
    }
}
