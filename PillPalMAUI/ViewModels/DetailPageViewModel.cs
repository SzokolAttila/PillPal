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
        public DetailPageViewModel(int userId, string auth)
        {
            HomeButton = new HomeButtonViewModel(userId, auth);
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
        private HomeButtonViewModel homeButton;
        public HomeButtonViewModel HomeButton
        {
            get => homeButton;
            set
            {
                homeButton = value;
                Changed();
            }
        }
    }
}
