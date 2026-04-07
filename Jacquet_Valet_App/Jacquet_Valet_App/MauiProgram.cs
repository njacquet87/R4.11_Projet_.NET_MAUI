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

        var httpClientBuilder = builder.Services.AddRefitClient<Interface.MovieInterface>();

        httpClientBuilder.ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://api.sampleapis.com");
        });
        
        builder.Services.AddTransient<ViewModels.MoviesViewModel>();
        builder.Services.AddTransient<Onglet2Page>();
        builder.Services.AddTransient<ViewModels.MoviesDetailViewModel>();
        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<ViewModels.CarouselViewModel>();
        builder.Services.AddTransient<Onglet1Page>();
        builder.Services.AddTransient<FilmsAVoirPage>();
        builder.Services.AddTransient<FilmsVusPage>();
        builder.Services.AddTransient<ViewModels.CarouselViewModel>();
        builder.Services.AddTransient<Onglet1Page>();
        builder.Services.AddTransient<FilmsAVoirPage>();
        builder.Services.AddTransient<Onglet4Page>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
