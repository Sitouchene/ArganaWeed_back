using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;



namespace ArganaWeedApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de connexion
            //Navigation.PushAsync(new LoginPage());
            //await Shell.Current.GoToAsync("//LoginPage");
            await Navigation.PushAsync(new LoginPage());
        }



        private async void OnLirePlus1Tapped(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://www.collegelacite.ca/bri/bio-innovation-cat-b");
            await Launcher.OpenAsync(uri);
        }

        private async void OnContactSalimTapped(object sender, EventArgs e)
        {
            try
            {
#if ANDROID || IOS
                var message = new EmailMessage
                {
                    Subject = "Contact",
                    Body = "",
                    To = new List<string> { "sitouchene@gmail.com" }
                };
                await Email.ComposeAsync(message);
#else
            await DisplayAlert("Non supporté", "Cette fonctionnalité n'est pas supportée sur cette plateforme.", "OK");
#endif
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Erreur", "Cette fonctionnalité n'est pas supportée sur cette plateforme.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur s'est produite: {ex.Message}", "OK");
            }
        }

 
    }
}

