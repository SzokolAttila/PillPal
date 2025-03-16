using PillPalLib;
using PillPalLib.APIHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
	public class UserRowViewModel : ViewModelBase
	{
		private User user;

		public User User
		{
			get { return user; }
			set { user = value; Changed(); Changed("ReminderCount"); }
		}

		private bool isVisible;

		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; Changed(); }
		}


		public string ReminderCount => User?.Reminders.Count + " db emlékeztető" ?? "";

		public ICommand Remove { get; set; }

		private readonly UserAPIHandler _userHandler = new();
		public string Auth { get; set; }
		public UserRowViewModel() {
			Remove = new Command(RemoveUser);
			IsVisible = true;
		}

		public async void RemoveUser() {
			bool resp = await Application.Current!.MainPage!.DisplayAlert("Felhasználó törlése", "Biztosan törölni szeretné a felhasználót?", "Igen", "Nem");
            if (resp)
			{
				_userHandler.DeleteUser(User!.Id, Auth);
				IsVisible = false;
			}
		}
	}
}
