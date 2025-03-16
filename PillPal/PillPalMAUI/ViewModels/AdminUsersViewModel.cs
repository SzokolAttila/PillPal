using PillPalLib.APIHandlers;
using PillPalMAUI.Resources.ContentViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class AdminUsersViewModel : ViewModelBase
    {
		private ObservableCollection<UserRowViewModel> userRows = new();

		public ObservableCollection<UserRowViewModel> UserRows
		{
			get { return userRows; }
			set { userRows = value; Changed(); }
		}

		private string searchText;

		public string SearchText
		{
			get { return searchText; }
			set { 
				searchText = value;
                Changed();
			}
		}

		public ICommand SearchUsers => new Command(() =>
        {
			UserRows.Clear();
			foreach(var user in _userHandler.GetUsers(Auth)
                .Where(x => x.UserName.ToLower().Contains(SearchText.ToLower()))
                .Select(x => new UserRowViewModel() { User = x, Auth = Auth }))
			{
				UserRows.Add(user);
			}

        });

        public string Auth { get; init; }
		private readonly UserAPIHandler _userHandler = new();
        public AdminUsersViewModel(string auth)
        {
            Auth = auth;
			UserRows = new();
			foreach (var user in _userHandler.GetUsers(auth)
                .Select(x => new UserRowViewModel() { User = x, Auth = Auth }))
			{
				UserRows.Add(user);
			}
        }
	}
}
