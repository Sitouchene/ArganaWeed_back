using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class VarietePage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<Variete> items;

        public VarietePage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<Variete>(dm.GetAllVarietes());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.VarieteNom.ToLower().Contains(searchText) ||
                                                 i.VarieteDescription.ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<Variete>(filteredItems);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as Variete;
            IdEntry.Text = selectedItem.VarieteId.ToString();
            CodeEntry.Text = selectedItem.VarieteCode;
            NomEntry.Text = selectedItem.VarieteNom;
            DescriptionEntry.Text = selectedItem.VarieteDescription;

            ItemsListView.SelectedItem = null;
        }

        private void OnValidateButtonClicked(object sender, EventArgs e)
        {
            string id = IdEntry.Text;
            string code = CodeEntry.Text;
            string nom = NomEntry.Text;
            string description = DescriptionEntry.Text;

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(description))
            {
                DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                dm.AddVariete(code, nom, description);
            }
            else
            {
                dm.UpdateVariete(int.Parse(id), code, nom, description);
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
            CodeEntry.Text = string.Empty;
            NomEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.CommandParameter;

            dm.DeleteVariete(id);
            LoadItems();
        }
    }
}
