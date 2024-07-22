using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class ProvenancePage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<Provenance> items;

        public ProvenancePage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<Provenance>(dm.GetAllProvenances());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.ProvenanceNom.ToLower().Contains(searchText) ||
                                                 i.ProvenanceDescription.ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<Provenance>(filteredItems);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as Provenance;
            IdEntry.Text = selectedItem.ProvenanceId.ToString();
            NomEntry.Text = selectedItem.ProvenanceNom;
            DescriptionEntry.Text = selectedItem.ProvenanceDescription;

            ItemsListView.SelectedItem = null;
        }

        private void OnValidateButtonClicked(object sender, EventArgs e)
        {
            string id = IdEntry.Text;
            string nom = NomEntry.Text;
            string description = DescriptionEntry.Text;

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(description))
            {
                DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                dm.AddProvenance(nom, description);
            }
            else
            {
                dm.UpdateProvenance(int.Parse(id), nom, description);
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
            NomEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.CommandParameter;

            dm.DeleteProvenance(id);
            LoadItems();
        }
    }
}
