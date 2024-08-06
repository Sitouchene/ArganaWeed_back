using Microsoft.Maui.Controls;
using ArganaWeedApp.ViewModels;
using ArganaWeedApp.Models;
using ArganaWeedApp.Views;

namespace ArganaWeedApp.Views
{
    public partial class PlantuleNewPage : ContentPage
    {
        public PlantuleNewPage()
        {
            InitializeComponent();
            var viewModel = new PlantuleNewViewModel();
            BindingContext = viewModel;
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleNewViewModel;
            if (viewModel != null)
            {
                await viewModel.SavePlantuleAsync();
            }
        }

        private void OnCancelButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleNewViewModel;
            viewModel?.Cancel();
        }

        private async void OnNavigateToDetailButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleNewViewModel;
            if (viewModel?.CreatedPlantule != null)
            {
                await NavigateToDetailAsync(viewModel.CreatedPlantule);
            }
        }

        private async Task NavigateToDetailAsync(PlantuleDetail plantule)
        {
            await Navigation.PushAsync(new PlantuleFichePage(plantule));
        }
    }
}
