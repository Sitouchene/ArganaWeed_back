using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class ProvenanceNewViewModel : BindableObject
    {
        private string _provenanceNom;
        public string ProvenanceNom
        {
            get => _provenanceNom;
            set
            {
                _provenanceNom = value;
                OnPropertyChanged();
            }
        }

        private string _provenanceDescription;
        public string ProvenanceDescription
        {
            get => _provenanceDescription;
            set
            {
                _provenanceDescription = value;
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

        public ICommand AddCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public ProvenanceNewViewModel()
        {
            AddCommand = new Command(async () => await AddProvenanceAsync());
            NavigateBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task AddProvenanceAsync()
        {
            ProvenanceNom = ProvenanceNom?.Trim();
            ProvenanceDescription = ProvenanceDescription?.Trim();

            if (string.IsNullOrWhiteSpace(ProvenanceNom) || ProvenanceNom.Length < 4 ||
                string.IsNullOrWhiteSpace(ProvenanceDescription) || ProvenanceDescription.Length < 4)
            {
                ErrorMessage = "Les champs doivent être renseignés et contenir au moins 4 caractères.";
                return;
            }

            var provenance = new Provenance
            {
                ProvenanceNom = ProvenanceNom,
                ProvenanceDescription = ProvenanceDescription
            };

            await ApiService.AddProvenanceAsync(provenance);

            MessagingCenter.Send(this, "RefreshProvenances");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
