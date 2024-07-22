using Microsoft.Maui.Controls;

namespace GestionPlantule.Pages
{
    public partial class AdminGestionPage : ContentPage
    {
        public AdminGestionPage()
        {
            InitializeComponent();
        }

        private async void OnEmployerButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmployerPage());
        }

        private async void OnVarieteButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VarietePage());
        }

        private async void OnEmplacementButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmplacementPage());
        }

        private async void OnProvenanceButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProvenancePage());
        }
    }
}
