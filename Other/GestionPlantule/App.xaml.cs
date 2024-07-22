using Microsoft.Maui.Controls;

namespace GestionPlantule
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GestionPlantule.Pages.Connexion());
        }
    }

}
