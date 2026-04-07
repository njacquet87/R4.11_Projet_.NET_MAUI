using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet4Page : ContentPage
{
    private readonly MoviesViewModel _viewModel;

    public Onglet4Page(MoviesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    async void OnButtonClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilmsAVoirPage));
    }
}