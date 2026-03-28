using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;

namespace Jacquet_Valet_App.ViewModels
{
    public partial class CarouselViewModel : ObservableObject
    {
        private readonly MovieInterface _movieInterface;

        [ObservableProperty]
        private ObservableCollection<MovieDto> randomMovies;

        public CarouselViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            randomMovies = new ObservableCollection<MovieDto>();
        }

        [RelayCommand]
        private async Task LoadRandomMovies()
        {

            RandomMovies.Clear();
            var random = new Random();
            var loadedMovieIds = new List<int>();

            while (RandomMovies.Count < 5)
            {
                var movieId = random.Next(1, 101); 
                if (loadedMovieIds.Contains(movieId)) continue;

                try
                {
                    var movie = await _movieInterface.GetMovieById(movieId);
                    if (movie != null)
                    {
                        RandomMovies.Add(movie);
                        loadedMovieIds.Add(movieId);
                    }
                }
                catch (Exception)
                {
                    // Ignore les films qui ne peuvent pas être chargés pour ne pas faire planter l'application.
                }
            }
        }
    }
}

