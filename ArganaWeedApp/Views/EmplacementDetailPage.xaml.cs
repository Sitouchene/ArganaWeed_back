using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class EmplacementDetailPage : ContentPage
    {
        public EmplacementDetailPage(Emplacement selectedEmplacement)
        {
            InitializeComponent();
            BindingContext = new EmplacementDetailViewModel(selectedEmplacement);
        }

        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EmplacementDetailViewModel;
            viewModel?.UpdateCommand.Execute(null);
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EmplacementDetailViewModel;
            viewModel?.DeleteCommand.Execute(null);
        }

        private void OnNavigateBackButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EmplacementDetailViewModel;
            viewModel?.NavigateBackCommand.Execute(null);
        }
    }
}
