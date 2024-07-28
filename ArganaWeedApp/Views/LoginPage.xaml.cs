using ArganaWeedApp.Models;
using ArganaWeedApp.Views;
using Microsoft.Maui.Controls;
using System;

namespace ArganaWeedApp.Views
{
    public partial class LoginPage : ContentPage
    {
        private UserRepository userRepository = new UserRepository();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            
            string usernameOrEmail = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string messageLable =MessageLabel.Text;

            var result = await userRepository.AuthenticateUserAsync(usernameOrEmail, password);

            


            if (result.Success)
            {
                //var menuPage = new MenuPage(result.UserRoles, result.UserId);
                var menuPage = new MenuPage(result.UserRoles,usernameOrEmail);

                await Navigation.PushAsync(menuPage);
                
             }
            else await DisplayAlert("Authentication", result.Message, "OK");
            UsernameEntry.Text = "";
            PasswordEntry.Text = "";


            /*
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            bool isAuthenticated = await AuthenticateUserAsync(username, password);

            if (isAuthenticated)
            {
                // Rediriger vers la page MenuPage après une authentification réussie
                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
                MessageLabel.Text = "Nom d'utilisateur ou mot de passe incorrect.";
            }*/
        }

        /*
        private Task<bool> AuthenticateUserAsync(string username, string password)
        {
            // Ajouter votre logique d'authentification ici
            // Retourner true si l'authentification est réussie, sinon false
            return Task.FromResult(username == "salim" && password == "123Soleil");
        }*/
    }
}

