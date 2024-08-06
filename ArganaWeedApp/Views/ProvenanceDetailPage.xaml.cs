using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class ProvenanceDetailPage : ContentPage
    {
        public ProvenanceDetailPage(Provenance selectedProvenance)
        {
            InitializeComponent();
            BindingContext = new ProvenanceDetailViewModel(selectedProvenance);
        }

        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProvenanceDetailViewModel;
            viewModel?.UpdateCommand.Execute(null);
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProvenanceDetailViewModel;
            viewModel?.DeleteCommand.Execute(null);
        }

        private void OnNavigateBackButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProvenanceDetailViewModel;
            viewModel?.NavigateBackCommand.Execute(null);
        }
    }
}
