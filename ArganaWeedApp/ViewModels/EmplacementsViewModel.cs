using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Views;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class EmplacementsViewModel : BindableObject
    {
        private ObservableCollection<Emplacement> _emplacements;
        public ObservableCollection<Emplacement> Emplacements
        {
            get => _emplacements;
            set
            {
                _emplacements = value;
                OnPropertyChanged();
            }
        }

        private Emplacement _selectedEmplacement;
        public Emplacement SelectedEmplacement
        {
            get => _selectedEmplacement;
            set
            {
                _selectedEmplacement = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NavigateToDetailCommand.Execute(value);
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                PerformSearch();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand AddEmplacementCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public EmplacementsViewModel()
        {
            Emplacements = new ObservableCollection<Emplacement>();
            RefreshCommand = new Command(async () => await LoadEmplacementsAsync());
            AddEmplacementCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(EmplacementNewPage)));
            NavigateToDetailCommand = new Command<Emplacement>(async (emplacement) => await NavigateToDetailAsync(emplacement));
            LoadEmplacementsAsync();
        }

        public async Task LoadEmplacementsAsync()
        {
            var emplacements = await ApiService.GetEmplacementsAsync();
            if (emplacements != null)
            {
                Emplacements.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                SearchText = string.Empty;
            }
        }

        private async void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadEmplacementsAsync();
                return;
            }

            var searchResults = await ApiService.SearchEmplacementsAsync(SearchText);
            if (searchResults != null)
            {
                Emplacements.Clear();
                foreach (var emplacement in searchResults)
                {
                    Emplacements.Add(emplacement);
                }
            }
        }

        private async Task NavigateToDetailAsync(Emplacement emplacement)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync($"{nameof(EmplacementDetailPage)}", true, new Dictionary<string, object>
                {
                    { "SelectedEmplacement", emplacement }
                });
            }
        }
    }
}
