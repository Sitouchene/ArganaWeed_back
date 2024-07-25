using System;
using Microsoft.Maui.Controls;


namespace ArganaWeedApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de connexion
            Navigation.PushAsync(new LoginPage());
        }

        

        private void OnLirePlusClicked(object sender, EventArgs e)
        {
            // Implémenter la logique de lecture supplémentaire
        }
    }
}
