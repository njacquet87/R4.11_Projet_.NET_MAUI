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

        [ObservableProperty]
        private string addToFilmsVusButtonText;

        public MoviesDetailViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            AddToFilmsAVoirButtonText = "Ajouter aux films à voir";
            AddToFilmsVusButtonText = "Ajouter aux films vus";
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
                AddToFilmsAVoirButtonText = "Ajouter aux films à voir";
                AddToFilmsVusButtonText = "Ajouter aux films vus";

                // récupération du film avec l'id en paramètre
                Movie = await _movieInterface.GetMovieById(MovieId);
            }
            catch (Exception ex)
            {
                // si le film n'est pas trouvé on prend depuis la collection partagée pour éviter de faire planter l'application
                Movie = MovieDataService.Movies.FirstOrDefault(m => m.Id == MovieId);
            }

            if (Movie != null)
            {
                UpdateButtonsState();
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

        [RelayCommand(CanExecute = nameof(CanAddToFilmsAVoir))]
        private void AddToFilmsAVoir()
        {
            if (Movie == null) return;

            // Si le film est dans les films vus, on le supprime de cette liste
            var filmVu = MovieDataService.FilmsVus.FirstOrDefault(m => m.Id == Movie.Id);
            if (filmVu != null)
            {
                MovieDataService.FilmsVus.Remove(filmVu);
            }

            // On ajoute le film à la liste "à voir" s'il n'y est pas déjà
            if (!MovieDataService.FilmsAVoir.Any(m => m.Id == Movie.Id))
            {
                MovieDataService.FilmsAVoir.Add(Movie);
            }
            
            UpdateButtonsState();
        }

        [RelayCommand(CanExecute = nameof(CanAddToFilmsVus))]
        private void AddToFilmsVus()
        {
            if (Movie == null) return;

            // Si le film est dans les films à voir, on le supprime de cette liste
            var filmAVoir = MovieDataService.FilmsAVoir.FirstOrDefault(m => m.Id == Movie.Id);
            if (filmAVoir != null)
            {
                MovieDataService.FilmsAVoir.Remove(filmAVoir);
            }

            // On ajoute le film à la liste "vus" s'il n'y est pas déjà
            if (!MovieDataService.FilmsVus.Any(m => m.Id == Movie.Id))
            {
                MovieDataService.FilmsVus.Add(Movie);
            }
            
            UpdateButtonsState();
        }

        private bool CanAddToFilmsAVoir() => Movie != null && !MovieDataService.FilmsAVoir.Any(m => m.Id == Movie.Id) && !MovieDataService.FilmsVus.Any(m => m.Id == Movie.Id);
        private bool CanAddToFilmsVus() => Movie != null && !MovieDataService.FilmsVus.Any(m => m.Id == Movie.Id);

        private void UpdateButtonsState()
        {
            AddToFilmsAVoirCommand.NotifyCanExecuteChanged();
            AddToFilmsVusCommand.NotifyCanExecuteChanged();
            
            if (MovieDataService.FilmsAVoir.Any(m => m.Id == MovieId))
            {
                AddToFilmsAVoirButtonText = "Film ajouté (à voir)";
            }
            else
            {
                AddToFilmsAVoirButtonText = "Ajouter aux films à voir";
            }

            if (MovieDataService.FilmsVus.Any(m => m.Id == MovieId))
            {
                AddToFilmsVusButtonText = "Film ajouté (vu)";
            }
            else
            {
                AddToFilmsVusButtonText = "Ajouter aux films vus";
            }
        }
    }
}
