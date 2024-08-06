using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class PlantulesPage : ContentPage
    {
        private readonly PdfService _pdfService;

        private PlantulesViewModel viewModel;
        public PlantulesPage()
        {
            InitializeComponent();
            _pdfService = new PdfService();
            viewModel = new PlantulesViewModel();
            BindingContext = viewModel;

            MessagingCenter.Subscribe<PlantuleFicheViewModel>(this, "RefreshPlantules", async (sender) =>
            {
                await viewModel.LoadPlantulesAsync();
            });

        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedPlantule = e.CurrentSelection[0] as PlantuleDetail;
                if (selectedPlantule != null)
                {
                    await Navigation.PushAsync(new PlantuleFichePage(selectedPlantule));
                }
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlantuleNewPage());
        }
        private async void OnGeneratePdfClicked(object sender, EventArgs e)
        {
            var title = "Rapport d'Inventaire";
            var content = "Contenu du rapport..."; // Vous pouvez ajouter ici le contenu dynamique

            var pdfData = _pdfService.GeneratePdf(title, content);

            var fileName = $"Rapport_Inventaire_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            File.WriteAllBytes(filePath, pdfData);

            await DisplayAlert("Succès", $"PDF généré : {filePath}", "OK");

            // Optionnel : Ouvrir le fichier PDF
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }
    }
}
