using ArganaWeedAppDevEx.Services;
using ArganaWeedAppDevEx.Views;
using Microsoft.Extensions.DependencyInjection;

using Application = Microsoft.Maui.Controls.Application;

namespace ArganaWeedAppDevEx
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get;  set; }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<NavigationService>();
            Routing.RegisterRoute(typeof(ItemDetailPage).FullName, typeof(ItemDetailPage));
            Routing.RegisterRoute(typeof(NewItemPage).FullName, typeof(NewItemPage));
            MainPage = new MainPage();
        }
    }
}
