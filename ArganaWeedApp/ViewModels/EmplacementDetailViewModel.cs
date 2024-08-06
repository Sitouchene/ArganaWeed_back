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
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
        }

        private async Task UpdateEmplacementAsync()
        {
            // Supprimer les espaces superflus
            Emplacement.EmplacementCode = Emplacement.EmplacementCode?.Trim();
            Emplacement.EmplacementDescription = Emplacement.EmplacementDescription?.Trim();

            // Vérifier si les champs sont remplis et ont au moins 4 caractères
            if (string.IsNullOrWhiteSpace(Emplacement.EmplacementCode) || Emplacement.EmplacementCode.Length < 4 ||
                string.IsNullOrWhiteSpace(Emplacement.EmplacementDescription) || Emplacement.EmplacementDescription.Length < 4)
            {
                ErrorMessage = "Les champs doivent être renseignés et contenir au moins 4 caractères.";
                return;
            }

            // Vérifier si au moins une valeur a été modifiée
            var originalEmplacement = await ApiService.GetEmplacementByIdAsync(Emplacement.EmplacementId);
            if (originalEmplacement != null &&
                originalEmplacement.EmplacementCode == Emplacement.EmplacementCode &&
                originalEmplacement.EmplacementDescription == Emplacement.EmplacementDescription)
            {
                ErrorMessage = "Veuillez modifier au moins un des champs.";
                return;
            }

            // Appel de l'API pour mettre à jour l'emplacement
            var updateResult = await ApiService.UpdateEmplacementAsync(Emplacement);
            if (updateResult != "Élément mis à jour avec succès.")
            {
                ErrorMessage = updateResult;
                return;
            }

            await AlertService.Instance.ShowAlert("Succès", updateResult, "OK");

            // Envoyer un message indiquant que la liste doit être rafraîchie
            //MessagingCenter.Send(this, "RefreshEmplacements");

            // Naviguer vers la page précédente
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteEmplacementAsync()
        {
            // Appel de l'API pour supprimer l'emplacement
            var deleteResult = await ApiService.DeleteEmplacementAsync(Emplacement.EmplacementId);

            if (deleteResult != "Élément supprimé avec succès.")
            {
                ErrorMessage = deleteResult;
                return;
            }

            await AlertService.Instance.ShowAlert("Succès", deleteResult, "OK");

            // Envoyer un message indiquant que la liste doit être rafraîchie
            //MessagingCenter.Send(this, "RefreshEmplacements");

            // Naviguer vers la page précédente
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Retour a la page pere et mise a jour des vues
        /// </summary>
        /// <returns></returns>
        private async Task NavigateBackAsync()
        {
            // Envoyer un message indiquant que la liste doit être rafraîchie
            MessagingCenter.Send(this, "RefreshEmplacements");

            // Naviguer vers la page précédente
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
