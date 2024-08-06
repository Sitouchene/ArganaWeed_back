using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class UserNewViewModel : BindableObject
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdministrator;
        public bool IsAdministrator
        {
            get => _isAdministrator;
            set
            {
                _isAdministrator = value;
                OnPropertyChanged();
            }
        }

        private bool _isOwner;
        public bool IsOwner
        {
            get => _isOwner;
            set
            {
                _isOwner = value;
                OnPropertyChanged();
            }
        }

        private bool _isAgent;
        public bool IsAgent
        {
            get => _isAgent;
            set
            {
                _isAgent = value;
                OnPropertyChanged();
            }
        }

        private bool _isViewer;
        public bool IsViewer
        {
            get => _isViewer;
            set
            {
                _isViewer = value;
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

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public UserNewViewModel()
        {
            SaveCommand = new Command(async () => await SaveUserAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        public async Task SaveUserAsync()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserEmail))
            {
                ErrorMessage = "All fields must be filled.";
                return;
            }

            var user = new User
            {
                UserName = UserName,
                UserEmail = UserEmail,
                HashedPassword = Password,
                IsActive = IsActive,
                IsAdministrator = IsAdministrator,
                IsOwner = IsOwner,
                IsAgent = IsAgent,
                IsViewer = IsViewer
            };

            await ApiService.AddUserAsync(user);
        }

        private async Task CancelAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
