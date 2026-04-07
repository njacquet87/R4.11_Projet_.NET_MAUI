using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;
using Jacquet_Valet_App.Services;

namespace Jacquet_Valet_App.ViewModels
{
    // utilisation de l'attribut QueryProperty pour lier la propriété MovieId à un paramètre de navigation nommé "id".
    [QueryProperty(nameof(MovieId), "id")]
    public partial class MoviesDetailViewModel : ObservableObject
    {
        private readonly MovieInterface _movieInterface;

        [ObservableProperty]
        private MovieDto movie;

        [ObservableProperty]
        private int movieId;

        [ObservableProperty]
        private string addToFilmsAVoirButtonText;

        public MoviesDetailViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            AddToFilmsAVoirButtonText = "Ajouter aux films à voir";
        }

        [RelayCommand]
        private async Task LoadMovie()
        {
            // On cherche le film avec son id 
            // Quand l'utilisateur ajoute un film, il n'est pas ajouté dans l'API la fonction getMovieById 
            // peut donc renvoyer une erreur. Si c'est le cas, on prend le film depuis la collection partagée
            // pour éviter de faire planter l'application
            try
            {
                if (MovieDataService.FilmsAVoir.Any(dto => dto.Id == Movie.Id))
                {
                    AddToFilmsAVoirButtonText = "Film ajouté";
                }

                // récupération du film avec l'id en paramètre
                Movie = await _movieInterface.GetMovieById(MovieId);
            }
            catch (Exception ex)
            {
                // si le film n'est pas trouvé on prend depuis la collection partagée pour éviter de faire planter l'application
                Movie = MovieDataService.Movies.FirstOrDefault(m => m.Id == MovieId);
            }
        }
        
        // Cette méthode est appelée automatiquement lorsque la propriété MovieId est modifiée.
        partial void OnMovieIdChanged(int value)
        {
            if (value > 0)
            {
                LoadMovieCommand.Execute(null);
            }
        }

        [RelayCommand]
        private void AddToFilmsAVoir()
        {
            if (Movie != null && !MovieDataService.FilmsAVoir.Any(m => m.Id == Movie.Id))
            {
                MovieDataService.FilmsAVoir.Add(Movie);
                AddToFilmsAVoirButtonText = "Film ajouté";
            }
        }
    }
}
