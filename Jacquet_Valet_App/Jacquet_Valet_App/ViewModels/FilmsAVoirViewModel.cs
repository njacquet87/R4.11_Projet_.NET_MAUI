using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Jacquet_Valet_App.Models;
using Jacquet_Valet_App.Services;

namespace Jacquet_Valet_App.ViewModels
{
    public partial class FilmsAVoirViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MovieDto> _filmsAVoir;

        public FilmsAVoirViewModel()
        {
            FilmsAVoir = MovieDataService.FilmsAVoir;
        }
    }
}


