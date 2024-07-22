using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using GestionPlantule.Models;

namespace GestionPlantule.Pages
{
    public partial class GestionPlantulesPage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        public ObservableCollection<Plantule> Plantules { get; set; }

        public GestionPlantulesPage()
        {
            InitializeComponent();
            LoadPlantules();
            UpdatePlantuleCounts();
        }

        private void LoadPlantules()
        {
            Plantules = new ObservableCollection<Plantule>(dm.GetAllPlantules().Select(p =>
            {
                p.SanteColor = GetSanteColor(p.Sante);
                return p;
            }));
            PlantulesListView.ItemsSource = Plantules;
        }

        private string GetSanteColor(string sante)
        {
            return sante switch
            {
                "Bon" => "Green",
                "Moyen" => "Orange",
                "Mauvais" => "Red",
                "En danger" => "DarkRed",
                _ => "Black"
            };
        }

        private void UpdatePlantuleCounts()
        {
            int activeCount = Plantules.Count(p => p.Statut);
            int totalCount = Plantules.Count;
            int inactiveCount = totalCount - activeCount;

            ActivePlantulesCountLabel.Text = $"{activeCount} / 2000";
            TotalPlantulesCountLabel.Text = $"{totalCount} / 2000";
            InactivePlantulesCountLabel.Text = $"{inactiveCount} / 2000";
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredPlantules = dm.GetAllPlantules()
                .Where(p => p.PlantuleVariete.ToLower().Contains(searchText) ||
                            p.PlantuleDescription.ToLower().Contains(searchText))
                .Select(p =>
                {
                    p.SanteColor = GetSanteColor(p.Sante);
                    return p;
                })
                .ToList();

            PlantulesListView.ItemsSource = new ObservableCollection<Plantule>(filteredPlantules);
        }

        private async void OnPlantuleItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedPlantule = e.Item as Plantule;
            if (selectedPlantule != null)
            {
                await Navigation.PushAsync(new FichePlantule(selectedPlantule));
            }
        }

        private async void OnAddNewPlantuleClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FichePlantule());
        }
    }
}
