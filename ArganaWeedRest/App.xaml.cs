using ArganaWeedRest.Views;
using Microsoft.Extensions.DependencyInjection;
namespace ArganaWeedRest
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            var dashboardPage = serviceProvider.GetRequiredService<DashboardPage>();
            MainPage = new NavigationPage(dashboardPage);
        }
    }
}
