using Microsoft.Extensions.Logging;
using ArganaWeedApp2.Services;

namespace ArganaWeedApp2
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
                });

            // Configuration de HttpClient
            builder.Services.AddHttpClient("ArganaWeedApi", client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5153/");
            });

            // Ajout de services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVarieteService, VarieteService>();
            builder.Services.AddScoped<IPlantuleService, PlantuleService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
