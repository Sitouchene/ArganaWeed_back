using ArganaWeedAppDevEx.Models;
using DevExpress.Maui.DataForm;

namespace ArganaWeedAppDevEx.ViewModels
{
    public class NewEmplacementViewModel : BaseViewModel
    {
        public const string ViewName = "NewEmplacementPage";

        string emplacementCode;
        string emplacementDescription;

        public NewEmplacementViewModel()
        {
            Title = "New Emplacement";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string EmplacementCode
        {
            get => this.emplacementCode;
            set => SetProperty(ref this.emplacementCode, value);
        }

        public string EmplacementDescription
        {
            get => this.emplacementDescription;
            set => SetProperty(ref this.emplacementDescription, value);
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }

        bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.emplacementCode)
                && !String.IsNullOrWhiteSpace(this.emplacementDescription);
        }

        async void OnCancel()
        {
            await Navigation.GoBackAsync();
        }

        async void OnSave()
        {
            Emplacement newEmplacement = new Emplacement()
            {
                EmplacementId = Guid.NewGuid().ToString(),
                EmplacementCode = EmplacementCode,
                EmplacementDescription = EmplacementDescription
            };

            await DataStore.AddItemAsync(newEmplacement);
            await Navigation.GoBackAsync();
        }
    }
}
