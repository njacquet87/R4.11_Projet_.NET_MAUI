using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class FilmsVusPage : ContentPage
{
    private readonly MoviesViewModel _viewModel;
    
    public FilmsVusPage(MoviesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}

