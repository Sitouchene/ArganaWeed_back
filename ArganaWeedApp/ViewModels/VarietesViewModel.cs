using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Views;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class VarietesViewModel : BindableObject
    {
        private ObservableCollection<Variete> _varietes;
        public ObservableCollection<Variete> Varietes
        {
            get => _varietes;
            set
            {
                _varietes = value;
                OnPropertyChanged();
            }
        }

        private Variete _selectedVariete;
        public Variete SelectedVariete
        {
            get => _selectedVariete;
            set
            {
                _selectedVariete = value;
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
        public ICommand AddVarieteCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public VarietesViewModel()
        {
            Varietes = new ObservableCollection<Variete>();
            RefreshCommand = new Command(async () => await LoadVarietesAsync());
            AddVarieteCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(VarieteNewPage)));
            NavigateToDetailCommand = new Command<Variete>(async (variete) => await NavigateToDetailAsync(variete));
            LoadVarietesAsync();
        }

        public async Task LoadVarietesAsync()
        {
            var varietes = await ApiService.GetVarietesAsync();
            if (varietes != null)
            {
                Varietes.Clear();
                foreach (var variete in varietes)
                {
                    Varietes.Add(variete);
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
                await LoadVarietesAsync();
                return;
            }

            var searchResults = await ApiService.SearchVarietesAsync(SearchText);
            if (searchResults != null)
            {
                Varietes.Clear();
                foreach (var variete in searchResults)
                {
                    Varietes.Add(variete);
                }
            }
        }

        private async Task NavigateToDetailAsync(Variete variete)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync($"{nameof(VarieteDetailPage)}", true, new Dictionary<string, object>
                {
                    { "SelectedVariete", variete }
                });
            }
        }
    }
}
