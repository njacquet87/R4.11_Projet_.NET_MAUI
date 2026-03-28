using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet2Page : ContentPage
{
    private readonly MoviesViewModel _viewModel;

    public Onglet2Page(MoviesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Charger les films lorsque la page apparaît
        _viewModel.LoadMoviesCommand.Execute(null);
    }
}