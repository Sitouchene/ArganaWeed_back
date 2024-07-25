using ArganaWeedAppDevEx.Models;
using System.Collections.ObjectModel;

namespace ArganaWeedAppDevEx.ViewModels
{
    public class EmplacementsViewModel : BaseViewModel
    {
        Emplacement _selectedEmplacement;

        public EmplacementsViewModel()
        {
            Title = "Browse Emplacements";
            Emplacements = new ObservableCollection<Emplacement>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
            ItemTapped = new Command<Emplacement>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        public ObservableCollection<Emplacement> Emplacements { get; }

        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }

        public Command<Emplacement> ItemTapped { get; }

        public Emplacement SelectedEmplacement
        {
            get => this._selectedEmplacement;
            set
            {
                SetProperty(ref this._selectedEmplacement, value);
                OnItemSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedEmplacement = null;
            ExecuteLoadItemsCommand();
        }

        void ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Emplacements.Clear();
                var emplacements = DataStore.GetItems(true);
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAddItem(object obj)
        {
            await Navigation.NavigateToAsync<NewEmplacementViewModel>(null);
        }

        async void OnItemSelected(Emplacement emplacement)
        {
            if (emplacement == null)
                return;
            await Navigation.NavigateToAsync<EmplacementDetailViewModel>(emplacement.EmplacementId);
        }
    }
}
