using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;
using ArganaWeedApp.Models;

namespace ArganaWeedApp.Views
{
    public partial class UserNewPage : ContentPage
    {
        public UserNewPage()
        {
            InitializeComponent();
            BindingContext = new UserNewViewModel();
        }

       private async void OnSaveClicked(object sender, EventArgs e)
        {
            var viewModel = (UserNewViewModel)BindingContext;
            await viewModel.SaveUserAsync();
            await Navigation.PopAsync();
        }


        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
