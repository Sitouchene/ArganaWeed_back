using ArganaWeedAppDevEx.Models;
using ArganaWeedAppDevEx.Services;
using DevExpress.Maui;
using DevExpress.Maui.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace ArganaWeedAppDevEx
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThemeManager.ApplyThemeToSystemBars = true;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress(useLocalization: true)
                .UseDevExpressControls()
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressEditors()
                .UseDevExpressDataGrid()
                .UseDevExpressScheduler()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                    fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                });

            var baseUrl = "http://10.0.2.2:5153"; // Valeur par défaut

            try
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var configFilePath = Path.Combine(basePath, "appsettings.maui.json");
                var devConfigFilePath = Path.Combine(basePath, $"appsettings.maui.{builder.Configuration["ASPNETCORE_ENVIRONMENT"]}.json");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                    .AddJsonFile(devConfigFilePath, optional: true)
                    .Build();

                baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des fichiers de configuration : {ex.Message}");
            }

            var apiConfiguration = new ApiConfiguration
            {
                BaseUrl = baseUrl
            };

            builder.Services.AddSingleton(apiConfiguration);
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddScoped<ApiService<Variete>>(provider => new ApiService<Variete>(provider.GetRequiredService<HttpClient>(), apiConfiguration.BaseUrl + "/api/varietes"));

            DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();
            DevExpress.Maui.DataGrid.Initializer.Init();
            DevExpress.Maui.Scheduler.Initializer.Init();

            var app = builder.Build();
            App.ServiceProvider = app.Services;
            return app;
        }
    }
}
