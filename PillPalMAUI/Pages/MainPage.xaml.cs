
using PillPalLib.DTOs.UserDTOs;
using PillPalMAUI.Pages;
using PillPalMAUI.Resources.ContentViews;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(int userId, string token)
        {
            InitializeComponent();
            BindingContext = new MainViewModel(userId, token);
        }
    }

}
