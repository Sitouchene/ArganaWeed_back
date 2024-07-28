using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class EmplacementDetailViewModel : BindableObject
    {
        private Emplacement _emplacement;
        public Emplacement Emplacement
        {
            get => _emplacement;
            set
            {
                _emplacement = value;
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

        public EmplacementDetailViewModel()
        {
            // Ajoutez une initialisation par défaut si nécessaire
        }

        public EmplacementDetailViewModel(Emplacement emplacement)
        {
            Emplacement = emplacement;
            UpdateCommand = new Command(async () => await UpdateEmplacementAsync());
            DeleteCommand = new Command(async () => await DeleteEmplacementAsync());
            NavigateBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task UpdateEmplacementAsync()
        {
            if (string.IsNullOrWhiteSpace(Emplacement.EmplacementCode) || string.IsNullOrWhiteSpace(Emplacement.EmplacementDescription))
            {
                ErrorMessage = "Tous les champs doivent être remplis.";
                return;
            }

            await ApiService.UpdateEmplacementAsync(Emplacement);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteEmplacementAsync()
        {
            await ApiService.DeleteEmplacementAsync(Emplacement.EmplacementId);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
