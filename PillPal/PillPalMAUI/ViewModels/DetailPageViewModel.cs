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
        public string Auth { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DetailPageViewModel()
        {
            HomeButton = new HomeButtonViewModel()
            { UserId = UserId, Auth = Auth };
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
        private HomeButtonViewModel homeButton = new();
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
