using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PillPalLib;

namespace PillPalMAUI.ViewModels
{
    public class ReminderCardViewModel : INotifyPropertyChanged
    {
        private Reminder _reminder;

        public Reminder Reminder
        {
            get { return _reminder; }
            set { _reminder = value; Changed(); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; Changed(); }
        }


        public ICommand Remove { get; set; }
        public ICommand Info { get; set; }
        public ICommand Edit { get; set; }

        public ReminderCardViewModel()
        {
            IsVisible = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Changed([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
