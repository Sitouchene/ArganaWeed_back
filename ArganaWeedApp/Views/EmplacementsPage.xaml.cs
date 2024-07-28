using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class EmplacementsPage : ContentPage
    {
        public EmplacementsPage()
        {
            InitializeComponent();
            BindingContext = new EmplacementsViewModel();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Emplacement selectedEmplacement)
            {
                await Navigation.PushAsync(new EmplacementDetailPage(selectedEmplacement));
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmplacementNewPage());
        }
    }
}
