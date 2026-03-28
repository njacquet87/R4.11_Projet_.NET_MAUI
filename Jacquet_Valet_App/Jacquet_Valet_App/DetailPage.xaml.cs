using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class DetailPage : ContentPage
{
    public DetailPage(MoviesDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}