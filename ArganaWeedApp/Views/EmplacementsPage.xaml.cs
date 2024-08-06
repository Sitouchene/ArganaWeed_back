using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class EmplacementsPage : ContentPage
    {
        private EmplacementsViewModel viewModel;
        public EmplacementsPage()
        {
            InitializeComponent();

            viewModel = new EmplacementsViewModel();
            BindingContext = viewModel;

            MessagingCenter.Subscribe<EmplacementDetailViewModel>(this, "RefreshEmplacements", async (sender) =>
            {
                await viewModel.LoadEmplacementsAsync();
            });

            /*
            MessagingCenter.Subscribe<EmplacementDetailViewModel, Emplacement>(this, "RefreshEmplacements", async (sender, emplacement) =>
            {
                await viewModel.LoadEmplacementsAsync();
            });

            MessagingCenter.Subscribe<EmplacementDetailViewModel, int>(this, "DeleteEmplacement", async (sender, emplacementId) =>
            {
                await viewModel.LoadEmplacementsAsync();
            });*/

        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Emplacement selectedEmplacement)
            {
                await Navigation.PushAsync(new EmplacementDetailPage(selectedEmplacement));
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmplacementNewPage());
        }
    }
}
