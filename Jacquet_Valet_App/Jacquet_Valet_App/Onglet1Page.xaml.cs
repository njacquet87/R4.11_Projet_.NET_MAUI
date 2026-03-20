namespace Jacquet_Valet_App;

public partial class Onglet1Page
{
    public Onglet1Page()
    {
        InitializeComponent();
    }

    async void OnButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GifPage));
    }
}