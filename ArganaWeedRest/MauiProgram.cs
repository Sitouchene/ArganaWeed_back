using ArganaWeedRest.Services;
using ArganaWeedRest.ViewModels;
using Microsoft.Extensions.Logging;
using ArganaWeedRest.Views;

namespace ArganaWeedRest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons-Regular");
                });
            
            builder.Services.AddHttpClient("ArganaWeedApiClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5153/api/statistics/"); // Remplacez par l'URL de votre API
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddTransient<IStatisticsService, StatisticsService>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddSingleton<App>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
