using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class UserDetailViewModel : BindableObject
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public UserDetailViewModel()
        {
            // Add default initialization if necessary
        }

        public UserDetailViewModel(User user)
        {
            User = user;
            UpdateCommand = new Command(async () => await UpdateUserAsync());
            DeleteCommand = new Command(async () => await DeleteUserAsync());
            NavigateBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task UpdateUserAsync()
        {
            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.UserEmail))
            {
                ErrorMessage = "All fields must be filled.";
                return;
            }

            //await ApiService.UpdateUserAsync(User);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteUserAsync()
        {
            //await ApiService.DeleteUserAsync(User.UserId);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
