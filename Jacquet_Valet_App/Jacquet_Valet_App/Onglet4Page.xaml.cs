using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet4Page : ContentPage
{
    private readonly MoviesViewModel _viewModel;
    private bool _hasLoadedMovies = false;

    public Onglet4Page(MoviesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    async void OnFilmsAVoirClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilmsAVoirPage));
    }

    async void OnFilmsVusClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilmsVusPage));
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Charger les films une seule fois lors du premier accès à la page
        if (!_hasLoadedMovies)
        {
            _viewModel.LoadMoviesCommand.Execute(null);
            _hasLoadedMovies = true;
        }
    }
}