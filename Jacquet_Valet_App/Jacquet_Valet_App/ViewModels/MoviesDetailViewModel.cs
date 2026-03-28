using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;

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

        public MoviesDetailViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
        }

        [RelayCommand]
        private async Task LoadMovie()
        {
            if (MovieId > 0)
            {
                // récupération du film avec l'id en paramètre
                Movie = await _movieInterface.GetMovieById(MovieId);
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
    }
}

