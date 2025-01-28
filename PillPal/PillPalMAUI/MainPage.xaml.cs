
using PillPalMAUI.Pages;
using PillPalMAUI.Resources.ContentViews;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJhZG1pbmlzdHJhdG9yIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MzgxNjQ4ODcsImlzcyI6Ik1pIGJvY3PDoXRqdWsga2kgYSB0b2tlbnQiLCJhdWQiOiJOZWtpayBhZGp1ayBraSBhIHRva2VudCJ9.gs30c3TKRRihmEuSXSFVeCbTf9XJWJJS5cK2GiUVVXw");
            Navigation.PushAsync(new LoginPage()).Wait();
        }
    }

}
