using Microsoft.Extensions.Logging;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jacquet_Valet_App;

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
        
        // Configuration des options JSON pour Refit
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        // Configuration de Refit pour l'API des bières
        var refitSettings = new RefitSettings
        {
            HttpMessageHandlerFactory = () => new HttpClientHandler(),
            ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
        };

        var httpClientBuilder = builder.Services.AddRefitClient<Interface.MovieInterface>(refitSettings);

        httpClientBuilder.ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://api.sampleapis.com");
            c.Timeout = TimeSpan.FromSeconds(30);
            c.DefaultRequestHeaders.Add("User-Agent", "Jacquet-Valet-App");
        });

        builder.Services.AddTransient<ViewModels.Onglet2ViewModel>();
        builder.Services.AddTransient<Onglet2Page>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

