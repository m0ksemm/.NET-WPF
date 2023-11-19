using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _4._Colors_WPF
{
    internal class ColorViewModel : ViewModelBase
    {
        private Color color;

        public ColorViewModel(Color _color) 
        {
            color = _color;
        }

        public string Name 
        {
            get { return color.Name; }
            set 
            {
                color.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

    }
}
