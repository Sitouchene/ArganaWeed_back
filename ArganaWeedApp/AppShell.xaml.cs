using ArganaWeedApp.Views;
namespace ArganaWeedApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(Views.MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(Views.MenuPage));
            Routing.RegisterRoute(nameof(PlantulesPage), typeof(Views.PlantulesPage));
            Routing.RegisterRoute(nameof(EmplacementsPage), typeof(Views.EmplacementsPage));
            Routing.RegisterRoute(nameof(EmplacementDetailPage), typeof(Views.EmplacementDetailPage));
            Routing.RegisterRoute(nameof(EmplacementNewPage), typeof(Views.EmplacementNewPage));
        }
    }
}
