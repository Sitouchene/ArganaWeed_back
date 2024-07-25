using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedAppDevEx.Services;
using Microsoft.Maui.Controls;
using ArganaWeedAppDevEx.Models;

namespace ArganaWeedAppDevEx.ViewModels
{
    public class PopupViewModel : BaseViewModel
    {
        private readonly ApiService<Variete> _apiService;

        public PopupViewModel(ApiService<Variete> apiService)
        {
            Title = "Popup";
            _apiService = apiService;
            CheckApiConnectionCommand = new Command(async () => await CheckApiConnection());
        }

        public ICommand CheckApiConnectionCommand { get; }

        private async Task CheckApiConnection()
        {
            try
            {
                var response = await _apiService.GetItemsAsync();
                if (response != null)
                {
                    Debug.WriteLine("API Response: Success");
                    await Application.Current.MainPage.DisplayAlert("Succès", "Connexion à l'API réussie", "OK");
                }
                else
                {
                    Debug.WriteLine("API Response: Null");
                    await Application.Current.MainPage.DisplayAlert("Erreur", "Échec de la connexion à l'API", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API Connection Error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Échec de la connexion à l'API: {ex.Message}", "OK");
            }
        }
    }
}
