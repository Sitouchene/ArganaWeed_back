using GestionPlantule.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace GestionPlantule
{
    public partial class MainPage : FlyoutPage
    {
        private dataManagement dm = new dataManagement();
        public MainPage()
        {
            InitializeComponent();
            userNameChange(dm.GetCurrentUser().UserName);
        }

        private async void OnPageClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string pageName = button.CommandParameter.ToString();
                Page page = pageName switch
                {
                    "GestionPlantules" => new Pages.GestionPlantulesPage(),
                    "ImportationFichier" => new Pages.ImportationFichierPage(),
                    "ScannerPlantule" => new Pages.ScannerPlantulePage(),
                    "AdminGestion" => new Pages.AdminGestionPage(),
                    "Historique" => new Pages.HistoriquePage(),
                    "Archivage" => new Pages.ArchivagePage(),
                    "Guide" => new Pages.GuidePage(),
                    _ => null
                };

                if (page != null)
                {
                    Detail = new NavigationPage(page);
                    // Fermer le menu flyout uniquement sur les appareils mobiles
                    if (DeviceInfo.Idiom == DeviceIdiom.Phone || DeviceInfo.Idiom == DeviceIdiom.Tablet)
                    {
                        IsPresented = false;
                    }
                }
            }
        }

        private void Deconnexion_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public void userNameChange(string currentUser)
        {
            string[] excusion = ["None", ""," "];
            if (currentUser != null || !excusion.Contains(currentUser))
            {
                UserName.Text = currentUser;
            }
            else
            {
               UserName.Text = "Pas d'utilisateur";
           }
        }
    }
}
