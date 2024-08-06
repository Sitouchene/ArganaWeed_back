using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class ProvenanceNewPage : ContentPage
    {
        public ProvenanceNewPage()
        {
            InitializeComponent();
            BindingContext = new ProvenanceNewViewModel();
        }

       

        private void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProvenanceNewViewModel;
            viewModel?.AddCommand.Execute(null);
        }
        private void OnCancelButton_CLicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProvenanceNewViewModel;
            viewModel?.NavigateBackCommand.Execute(null);
        }
    }
}
