using PillPalLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalMAUI.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel()
        {

        }
        private Medicine medicine = new();
        public Medicine Medicine
        {
            get => medicine;
            set
            {
                medicine = value;
                Changed();
            }
        }
    }
}
