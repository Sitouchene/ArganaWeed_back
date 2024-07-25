using ArganaWeedAppDevEx.ViewModels;
using ArganaWeedAppDevEx.Models;
using ArganaWeedAppDevEx.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ArganaWeedAppDevEx.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupPage : ContentPage
    {
        public PopupPage()
        {
            InitializeComponent();

            // Résoudre les dépendances via le conteneur de services
            var viewModel = (PopupViewModel)Activator.CreateInstance(typeof(PopupViewModel), App.ServiceProvider.GetService<ApiService<Variete>>());
            BindingContext = viewModel;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            Popup.IsOpen = true;
        }
    }
}
