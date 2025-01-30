using PillPalMAUI.Resources.Styles;
using PillPalMAUI.Resources.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalMAUI.ViewModels
{
    class ThemeSwitchViewModel : BaseViewModel
    {
        public ThemeSwitchViewModel()
        {

        }
        private static bool isToggled = true;
        public bool IsToggled
        {
            get => isToggled;
            set
            {
                isToggled = value;
                Changed();
                SwitchTheme();
            }
        }
        private void SwitchTheme()
        {
            var dictionaries = Application.Current!.Resources.MergedDictionaries;
            if (dictionaries != null)
            {
                dictionaries.Clear();
                dictionaries.Add(new CustomStyles());
                if (!isToggled)
                    dictionaries.Add(new DarkTheme());
                else dictionaries.Add(new LightTheme());
            }
        }
    }
}
