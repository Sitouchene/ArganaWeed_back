using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class LaboInfoDetailViewModel : BindableObject
    {
        private LaboInfo _laboInfo;
        public LaboInfo LaboInfo
        {
            get => _laboInfo;
            set
            {
                _laboInfo = value;
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
        public ICommand CancelCommand { get; }

        public LaboInfoDetailViewModel()
        {
            UpdateCommand = new Command(async () => await UpdateLaboInfoAsync());
            CancelCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
            LoadLaboInfoAsync();
        }

        private async void LoadLaboInfoAsync()
        {
            try
            {
                LaboInfo = await ApiService.GetLaboInfoAsync();
                if (LaboInfo == null)
                {
                    ErrorMessage = "Erreur lors du chargement des informations du laboratoire.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur: {ex.Message}";
            }
        }

        private async Task UpdateLaboInfoAsync()
        {
            if (LaboInfo == null)
            {
                ErrorMessage = "Les informations du laboratoire ne sont pas disponibles.";
                return;
            }

            if (LaboInfo.CapaciteLabo <= 0)
            {
                ErrorMessage = "Capacité du Labo doit être un nombre entier positif.";
                return;
            }

            if (LaboInfo.CapaciteLicence <= 0)
            {
                ErrorMessage = "Capacité de la Licence doit être un nombre entier positif.";
                return;
            }

            if (string.IsNullOrWhiteSpace(LaboInfo.NomLabo) || LaboInfo.NomLabo.Length < 3)
            {
                ErrorMessage = "Nom du Laboratoire doit contenir au moins 3 caractères.";
                return;
            }

            try
            {
                var result = await ApiService.UpdateLaboInfoAsync(LaboInfo);

                if (result != "LaboInfo mis à jour avec succès!")
                {
                    ErrorMessage = result;
                    return;
                }

                //await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de la mise à jour: {ex.Message}";
            }
        }
    }
}
