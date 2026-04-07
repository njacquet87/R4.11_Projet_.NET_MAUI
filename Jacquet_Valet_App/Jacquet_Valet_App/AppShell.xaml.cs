namespace Jacquet_Valet_App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(Onglet1Page), typeof(Onglet1Page));
        Routing.RegisterRoute(nameof(Onglet2Page), typeof(Onglet2Page));
        Routing.RegisterRoute(nameof(Onglet3Page), typeof(Onglet3Page));
        Routing.RegisterRoute(nameof(Onglet4Page), typeof(Onglet4Page));
        
        Routing.RegisterRoute(nameof(GifPage), typeof(GifPage));
        Routing.RegisterRoute("detail", typeof(DetailPage));
        Routing.RegisterRoute(nameof(FilmsAVoirPage), typeof(FilmsAVoirPage));
        Routing.RegisterRoute(nameof(FilmsVusPage), typeof(FilmsVusPage));
    }
}