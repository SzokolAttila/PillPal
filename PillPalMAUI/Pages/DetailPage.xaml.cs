using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class DetailPage : ContentPage
{
	public DetailPage(Medicine medicine)
	{
		InitializeComponent();
        BindingContext = new DetailPageViewModel() { Medicine = medicine };
    }
}