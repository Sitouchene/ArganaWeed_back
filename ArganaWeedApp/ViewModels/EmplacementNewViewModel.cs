using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class EmplacementNewViewModel : BindableObject
    {
        private string _emplacementCode;
        public string EmplacementCode
        {
            get => _emplacementCode;
            set
            {
                _emplacementCode = value;
                OnPropertyChanged();
            }
        }

        private string _emplacementDescription;
        public string EmplacementDescription
        {
            get => _emplacementDescription;
            set
            {
                _emplacementDescription = value;
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

        public EmplacementNewViewModel()
        {
            SaveCommand = new Command(async () => await SaveEmplacementAsync());
            CancelCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task SaveEmplacementAsync()
        {
            if (string.IsNullOrWhiteSpace(EmplacementCode) || string.IsNullOrWhiteSpace(EmplacementDescription))
            {
                ErrorMessage = "Tous les champs doivent être remplis.";
                return;
            }

            var emplacement = new Emplacement
            {
                EmplacementCode = EmplacementCode,
                EmplacementDescription = EmplacementDescription
            };

            await ApiService.AddEmplacementAsync(emplacement);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
