using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class LaboInfoDetailPage : ContentPage
    {
        public LaboInfoDetailPage()
        {
            InitializeComponent();
            BindingContext = new LaboInfoDetailViewModel();
        }

        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as LaboInfoDetailViewModel;
            viewModel?.UpdateCommand.Execute(null);
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as LaboInfoDetailViewModel;
            viewModel?.CancelCommand.Execute(null);
        }
    }
}
