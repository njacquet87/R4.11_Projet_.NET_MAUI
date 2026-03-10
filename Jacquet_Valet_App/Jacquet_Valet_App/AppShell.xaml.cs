namespace Jacquet_Valet_App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("onglet1", typeof(Onglet1Page));
        Routing.RegisterRoute("onglet2", typeof(Onglet2Page));
        Routing.RegisterRoute("onglet3", typeof(Onglet3Page));
        Routing.RegisterRoute("onglet4", typeof(Onglet4Page));
    }
}