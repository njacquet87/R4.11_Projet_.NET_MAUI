using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet2Page : ContentPage
{
    private readonly Onglet2ViewModel _viewModel;

    public Onglet2Page(Onglet2ViewModel viewModel)
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