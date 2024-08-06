using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Views;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class UsersViewModel : BindableObject
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NavigateToDetailCommand.Execute(value);
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                PerformSearch();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public UsersViewModel()
        {
            Users = new ObservableCollection<User>();
            RefreshCommand = new Command(async () => await LoadUsersAsync());
            AddUserCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(UserNewPage)));
            NavigateToDetailCommand = new Command<User>(async (user) => await NavigateToDetailAsync(user));
            LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            var users = await ApiService.GetUsersAsync();
            if (users != null)
            {
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                SearchText = string.Empty;
            }
        }

        private async void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadUsersAsync();
                return;
            }

            var searchResults = await ApiService.SearchUsersAsync(SearchText);
            if (searchResults != null)
            {
                Users.Clear();
                foreach (var user in searchResults)
                {
                    Users.Add(user);
                }
            }
        }

        private async Task NavigateToDetailAsync(User user)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}", true, new Dictionary<string, object>
                {
                    { "SelectedUser", user }
                });
            }
        }
    }
}
