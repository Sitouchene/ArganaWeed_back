using ArganaWeedApp.Views;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["Secondary"],
                BarTextColor = (Color)Application.Current.Resources["Primary"]
            };

            MainPage = navigationPage;
        }
    }
}
