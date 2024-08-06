using Microsoft.Maui.Controls;
using ArganaWeedApp.ViewModels;

namespace ArganaWeedApp.Views
{
    public partial class ImportPlantulesPage : ContentPage
    {
        public ImportPlantulesPage()
        {
            InitializeComponent();
            var viewModel = new ImportPlantulesViewModel();
            BindingContext = viewModel;
        }
    }
}
