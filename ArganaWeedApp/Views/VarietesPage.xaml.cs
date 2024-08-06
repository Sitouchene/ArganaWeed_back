using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class VarietesPage : ContentPage
    {
        private VarietesViewModel viewModel;
        public VarietesPage()
        {
            InitializeComponent();

            viewModel = new VarietesViewModel();
            BindingContext = viewModel;

            MessagingCenter.Subscribe<VarieteDetailViewModel>(this, "RefreshVarietes", async (sender) =>
            {
                await viewModel.LoadVarietesAsync();
            });

           

        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Variete selectedVariete)
            {
                await Navigation.PushAsync(new VarieteDetailPage(selectedVariete));
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VarieteNewPage());
        }
    }
}
