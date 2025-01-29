
using PillPalMAUI.Resources.Styles;
using PillPalMAUI.Resources.Themes;

namespace PillPalMAUI.Resources.ContentViews;

public partial class ThemeSwitch : ContentView
{
	private static bool isLight = true;
	public ThemeSwitch()
	{
		InitializeComponent();
		var dictionaries = Application.Current!.Resources.MergedDictionaries;
		if (dictionaries != null)
		{
			Toggle.IsToggled = isLight;
		}
	}

    private void SwitchTheme(object sender, ToggledEventArgs e)
    {
		#if ANDROID
		var dictionaries = Application.Current!.Resources.MergedDictionaries;
		if (dictionaries != null)
		{
			dictionaries.Clear();
			dictionaries.Add(new CustomStyles());
			if (!Toggle.IsToggled)
				dictionaries.Add(new DarkTheme());
			else dictionaries.Add(new LightTheme());
			isLight = Toggle.IsToggled;
		}
		#endif
	}
}