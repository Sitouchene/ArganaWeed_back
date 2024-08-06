using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class ProvenanceDetailViewModel : BindableObject
    {
        private Provenance _provenance;
        public Provenance Provenance
        {
            get => _provenance;
            set
            {
                _provenance = value;
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

        public ProvenanceDetailViewModel()
        {
            // Default initialization if necessary
        }

        public ProvenanceDetailViewModel(Provenance provenance)
        {
            Provenance = provenance;
            UpdateCommand = new Command(async () => await UpdateProvenanceAsync());
            DeleteCommand = new Command(async () => await DeleteProvenanceAsync());
            NavigateBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task UpdateProvenanceAsync()
        {
            // Trim the input fields
            Provenance.ProvenanceNom = Provenance.ProvenanceNom?.Trim();
            Provenance.ProvenanceDescription = Provenance.ProvenanceDescription?.Trim();

            // Validate the input fields
            if (string.IsNullOrWhiteSpace(Provenance.ProvenanceNom) || Provenance.ProvenanceNom.Length < 4 ||
                string.IsNullOrWhiteSpace(Provenance.ProvenanceDescription) || Provenance.ProvenanceDescription.Length < 4)
            {
                ErrorMessage = "Les champs doivent être renseignés et contenir au moins 4 caractères.";
                return;
            }

            // Check if at least one value has changed
            var originalProvenance = await ApiService.GetProvenanceByIdAsync(Provenance.ProvenanceId);
            if (originalProvenance != null &&
                originalProvenance.ProvenanceNom == Provenance.ProvenanceNom &&
                originalProvenance.ProvenanceDescription == Provenance.ProvenanceDescription)
            {
                ErrorMessage = "Veuillez modifier au moins un des champs.";
                return;
            }

            // Call the API to update the provenance
            var updateResult = await ApiService.UpdateProvenanceAsync(Provenance);
            if (updateResult != "Élément mis à jour avec succès.")
            {
                ErrorMessage = updateResult;
                return;
            }

            // Show success alert and refresh the view (e.g., a ListView)
            await AlertService.Instance.ShowAlert("Succès", updateResult, "OK");
            MessagingCenter.Send(this, "RefreshProvenances");

            // Navigate to the previous page
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteProvenanceAsync()
        {
            // Call the API to delete the provenance
            var deleteResult = await ApiService.DeleteProvenanceAsync(Provenance.ProvenanceId);

            if (deleteResult != "Élément supprimé avec succès.")
            {
                ErrorMessage = deleteResult;
                return;
            }

            // Show success alert and refresh the view (e.g., a ListView)
            await AlertService.Instance.ShowAlert("Succès", deleteResult, "OK");
            MessagingCenter.Send(this, "RefreshProvenances");

            // Navigate to the previous page
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
