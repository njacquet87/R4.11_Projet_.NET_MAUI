using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet1Page
{
    private readonly CarouselViewModel _viewModel;
    public Onglet1Page(CarouselViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // On appelle la commande pour charger les films quand la page apparaît
        _viewModel.LoadRandomMoviesCommand.Execute(null);
    }

    async void OnButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GifPage));
    }
}