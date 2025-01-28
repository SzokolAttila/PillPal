
using PillPalMAUI.Resources.ContentViews;

namespace PillPalMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Stack.Add(new LogoContentView());
        }
    }

}
