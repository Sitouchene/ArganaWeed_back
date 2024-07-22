using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class EmployerPage : ContentPage
    {
        private dataManagement dm = new dataManagement();
        private ObservableCollection<User> items;

        public EmployerPage()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = new ObservableCollection<User>(dm.GetAllUsers());
            ItemsListView.ItemsSource = items;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();
            var filteredItems = items.Where(i => i.UserName.ToLower().Contains(searchText) ||
                                                 i.Statut.ToLower().Contains(searchText));
            ItemsListView.ItemsSource = new ObservableCollection<User>(filteredItems);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as User;
            IdEntry.Text = selectedItem.UserId.ToString();
            NomEntry.Text = selectedItem.UserName;
            PrenomEntry.Text = selectedItem.UserEmail;
            StatutPicker.SelectedItem = selectedItem.Statut;

            ItemsListView.SelectedItem = null;
        }

        private void OnValidateButtonClicked(object sender, EventArgs e)
        {
            string id = IdEntry.Text;
            string nom = NomEntry.Text;
            string prenom = PrenomEntry.Text;
            string statut = StatutPicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(statut))
            {
                DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                dm.CreateUser(nom, "password", prenom, statut);
            }
            else
            {
                dm.UpdateUser(int.Parse(id), nom, prenom, statut);
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
            PrenomEntry.Text = string.Empty;
            StatutPicker.SelectedIndex = -1;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.CommandParameter;

            dm.DeleteUser(id);
            LoadItems();
        }
    }
}
