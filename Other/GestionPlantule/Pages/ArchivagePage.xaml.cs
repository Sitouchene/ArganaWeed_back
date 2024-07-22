using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class ArchivagePage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<Plantule> items;

        public ArchivagePage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<Plantule>(dm.GetArchivedPlantules());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.PlantuleVariete.ToLower().Contains(searchText) ||
                                                 i.PlantuleDescription.ToLower().Contains(searchText) ||
                                                 i.DateReception.ToString("g").ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<Plantule>(filteredItems);
        }
    }
}
