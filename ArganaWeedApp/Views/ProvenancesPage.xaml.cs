using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class ProvenancesPage : ContentPage
    {
        private ProvenancesViewModel viewModel;

        public ProvenancesPage()
        {
            InitializeComponent();
            viewModel = new ProvenancesViewModel();
            BindingContext = viewModel;

            MessagingCenter.Subscribe<ProvenanceDetailViewModel, Provenance>(this, "UpdateProvenance", async (sender, provenance) =>
            {
                await viewModel.LoadProvenancesAsync();
            });

            MessagingCenter.Subscribe<ProvenanceDetailViewModel, int>(this, "DeleteProvenance", async (sender, provenanceId) =>
            {
                await viewModel.LoadProvenancesAsync();
            });
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Provenance selectedProvenance)
            {
                await Navigation.PushAsync(new ProvenanceDetailPage(selectedProvenance));
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProvenanceNewPage());
        }
    }
}
