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
    }
}
