using System.Collections.ObjectModel;
using Jacquet_Valet_App.Models;

namespace Jacquet_Valet_App.Services
{
    public static class MovieDataService
    {
        public static ObservableCollection<MovieDto> Movies { get; } = new ObservableCollection<MovieDto>();
        public static ObservableCollection<MovieDto> FilmsAVoir { get; } = new ObservableCollection<MovieDto>();
        public static ObservableCollection<MovieDto> FilmsVus { get; } = new ObservableCollection<MovieDto>();
    }
}