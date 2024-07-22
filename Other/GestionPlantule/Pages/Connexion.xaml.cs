using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using System;

namespace GestionPlantule.Pages
{
    public partial class Connexion : ContentPage
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private dataManagement dm = new dataManagement();

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string userEmail = UserEmailEntry.Text;
            string password = PasswordEntry.Text;

            // Simuler une vérification des informations d'identification
            if (IsValidUser(userEmail, password))
            {
                // Naviguer vers la page principale en remplaçant la page actuelle
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Erreur", "Email ou mot de passe incorrect.", "OK");
            }
        }

        // Méthode simulant la validation des informations d'identification
        private bool IsValidUser(string email, string password)
        {
            if (!dm.AuthenticateUser("Admin", "Admin"))
            {
                dm.CreateUser("Admin", "Admin", "Admin", "Admin");
                DisplayAlert("Admin", "Admin", "OK");
            }

            // Logique de validation à implémenter
            return dm.AuthenticateUser(email, password);
        }
    }
}
