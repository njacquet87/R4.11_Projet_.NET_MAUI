using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class FilmsAVoirPage : ContentPage
{
    private readonly FilmsAVoirViewModel _viewModel;
    
    public FilmsAVoirPage(FilmsAVoirViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}

