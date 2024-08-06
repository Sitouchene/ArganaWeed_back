using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class VarieteDetailViewModel : BindableObject
    {
        private Variete _variete;
        public Variete Variete
        {
            get => _variete;
            set
            {
                _variete = value;
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

        public VarieteDetailViewModel()
        {
            // Ajoutez une initialisation par défaut si nécessaire
        }

        public VarieteDetailViewModel(Variete variete)
        {
            Variete = variete;
            UpdateCommand = new Command(async () => await UpdateVarieteAsync());
            DeleteCommand = new Command(async () => await DeleteVarieteAsync());
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
        }

        private async Task UpdateVarieteAsync()
        {
            // Supprimer les espaces superflus
            Variete.VarieteCode = Variete.VarieteCode?.Trim();
            Variete.VarieteDescription = Variete.VarieteDescription?.Trim();

            // Vérifier si les champs sont remplis et ont au moins 4 caractères
            if (string.IsNullOrWhiteSpace(Variete.VarieteCode) || Variete.VarieteCode.Length < 4 ||
                string.IsNullOrWhiteSpace(Variete.VarieteDescription) || Variete.VarieteDescription.Length < 4||
                string.IsNullOrWhiteSpace(Variete.VarieteNom) || Variete.VarieteNom.Length < 4)
            {
                ErrorMessage = "Les champs doivent être renseignés et contenir au moins 4 caractères.";
                return;
            }

            // Vérifier si au moins une valeur a été modifiée
            var originalVariete = await ApiService.GetVarieteByIdAsync(Variete.VarieteId);
            if (originalVariete != null &&
                originalVariete.VarieteCode == Variete.VarieteCode &&
                originalVariete.VarieteDescription == Variete.VarieteDescription)
            {
                ErrorMessage = "Veuillez modifier au moins un des champs.";
                return;
            }

            // Appel de l'API pour mettre à jour l'variete
            var updateResult = await ApiService.UpdateVarieteAsync(Variete);
            if (updateResult != "Élément mis à jour avec succès.")
            {
                ErrorMessage = updateResult;
                return;
            }

            await AlertService.Instance.ShowAlert("Succès", updateResult, "OK");

            // Envoyer un message indiquant que la liste doit être rafraîchie
            //MessagingCenter.Send(this, "RefreshVarietes");

            // Naviguer vers la page précédente
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteVarieteAsync()
        {
            // Appel de l'API pour supprimer l'variete
            var deleteResult = await ApiService.DeleteVarieteAsync(Variete.VarieteId);

            if (deleteResult != "Élément supprimé avec succès.")
            {
                ErrorMessage = deleteResult;
                return;
            }

            await AlertService.Instance.ShowAlert("Succès", deleteResult, "OK");

            // Envoyer un message indiquant que la liste doit être rafraîchie
            //MessagingCenter.Send(this, "RefreshVarietes");

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
            MessagingCenter.Send(this, "RefreshVarietes");

            // Naviguer vers la page précédente
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
