
using PillPalMAUI.Pages;
using PillPalMAUI.Resources.ContentViews;

namespace PillPalMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Navigation.PushAsync(new LoginPage()).Wait();
            var view = new VerticalStackLayout();
            view.Children.Add(new ReminderCard());
            Content = view;
        }
    }

}
