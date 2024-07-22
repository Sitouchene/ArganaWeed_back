using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class EmplacementPage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<Emplacement> items;

        public EmplacementPage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<Emplacement>(dm.GetAllEmplacements());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.EmplacementCode.ToLower().Contains(searchText) ||
                                                 i.EmplacementDescription.ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<Emplacement>(filteredItems);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as Emplacement;
            IdEntry.Text = selectedItem.EmplacementId.ToString();
            EmplacementCodeEntry.Text = selectedItem.EmplacementCode;
            DescriptionEntry.Text = selectedItem.EmplacementDescription;
            StorageMaxEntry.Text = selectedItem.StorageMax.ToString();

            ItemsListView.SelectedItem = null;
        }

        private void OnValidateButtonClicked(object sender, EventArgs e)
        {
            string id = IdEntry.Text;
            string code = EmplacementCodeEntry.Text;
            string description = DescriptionEntry.Text;
            int storageMax;

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(description) || !int.TryParse(StorageMaxEntry.Text, out storageMax))
            {
                DisplayAlert("Validation Error", "Please fill in all fields correctly.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                dm.AddEmplacement(code, description, storageMax);
            }
            else
            {
                dm.UpdateEmplacement(int.Parse(id), code, description, storageMax);
            }

            LoadItems();
            ClearFields();
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            IdEntry.Text = string.Empty;
            EmplacementCodeEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
            StorageMaxEntry.Text = string.Empty;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.CommandParameter;

            dm.DeleteEmplacement(id);
            LoadItems();
        }
    }
}
