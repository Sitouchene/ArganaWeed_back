using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class VarieteDetailPage : ContentPage
    {
        public VarieteDetailPage(Variete selectedVariete)
        {
            InitializeComponent();
            BindingContext = new VarieteDetailViewModel(selectedVariete);
        }

        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VarieteDetailViewModel;
            viewModel?.UpdateCommand.Execute(null);
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VarieteDetailViewModel;
            viewModel?.DeleteCommand.Execute(null);
        }

        private void OnNavigateBackButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VarieteDetailViewModel;
            viewModel?.NavigateBackCommand.Execute(null);
        }
    }
}
