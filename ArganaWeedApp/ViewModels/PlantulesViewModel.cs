using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Views;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class PlantulesViewModel : BindableObject
    {
        private ObservableCollection<PlantuleDetail> _plantules;
        public ObservableCollection<PlantuleDetail> Plantules
        {
            get => _plantules;
            set
            {
                _plantules = value;
                OnPropertyChanged();
            }
        }

        private PlantuleDetail _selectedPlantule;
        public PlantuleDetail SelectedPlantule
        {
            get => _selectedPlantule;
            set
            {
                _selectedPlantule = value;
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
        public ICommand AddPlantuleCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public PlantulesViewModel()
        {
            Plantules = new ObservableCollection<PlantuleDetail>();
            RefreshCommand = new Command(async () => await LoadPlantulesAsync());
            AddPlantuleCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(PlantuleNewPage)));
            NavigateToDetailCommand = new Command<PlantuleDetail>(async (plantule) => await NavigateToDetailAsync(plantule));
            LoadPlantulesAsync();
        }

        public async Task LoadPlantulesAsync()
        {
            var plantules = await ApiService.GetPlantulesActiveAsync();
            if (plantules != null)
            {
                Plantules.Clear();
                foreach (var plantule in plantules)
                {
                    Plantules.Add(plantule);
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
                await LoadPlantulesAsync();
                return;
            }

            var searchResults = await ApiService.SearchPlantulesAsync(SearchText);
            if (searchResults != null)
            {
                Plantules.Clear();
                foreach (var plantule in searchResults)
                {
                    Plantules.Add(plantule);
                }
            }
        }

        private async Task NavigateToDetailAsync(PlantuleDetail plantule)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync($"{nameof(PlantuleFichePage)}", true, new Dictionary<string, object>
                {
                    { "SelectedPlantule", plantule }
                });
            }
        }

    }


}
