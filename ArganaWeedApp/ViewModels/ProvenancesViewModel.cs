using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using ArganaWeedApp.Views;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class ProvenancesViewModel : BindableObject
    {
        private ObservableCollection<Provenance> _provenances;
        public ObservableCollection<Provenance> Provenances
        {
            get => _provenances;
            set
            {
                _provenances = value;
                OnPropertyChanged();
            }
        }

        private Provenance _selectedProvenance;
        public Provenance SelectedProvenance
        {
            get => _selectedProvenance;
            set
            {
                _selectedProvenance = value;
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
        public ICommand AddProvenanceCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public ProvenancesViewModel()
        {
            Provenances = new ObservableCollection<Provenance>();
            RefreshCommand = new Command(async () => await LoadProvenancesAsync());
            AddProvenanceCommand = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new ProvenanceNewPage()));
            NavigateToDetailCommand = new Command<Provenance>(async (provenance) => await NavigateToDetailAsync(provenance));
            LoadProvenancesAsync();
        }

        public async Task LoadProvenancesAsync()
        {
            var provenances = await ApiService.GetProvenancesAsync();
            if (provenances != null)
            {
                Provenances.Clear();
                foreach (var provenance in provenances)
                {
                    Provenances.Add(provenance);
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
                await LoadProvenancesAsync();
                return;
            }

            var searchResults = await ApiService.SearchProvenancesAsync(SearchText);
            if (searchResults != null)
            {
                Provenances.Clear();
                foreach (var provenance in searchResults)
                {
                    Provenances.Add(provenance);
                }
            }
        }

        private async Task NavigateToDetailAsync(Provenance provenance)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProvenanceDetailPage(provenance));
        }
    }
}
