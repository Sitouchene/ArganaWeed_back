using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class HistoriquePage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<Event> items;

        public HistoriquePage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<Event>(dm.GetAllEvents());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.EventUserName.ToLower().Contains(searchText) ||
                                                 i.EventSource.ToLower().Contains(searchText) ||
                                                 i.EventLog.ToLower().Contains(searchText) ||
                                                 i.EventDatetime.ToString("g").ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<Event>(filteredItems);
        }
    }
}
