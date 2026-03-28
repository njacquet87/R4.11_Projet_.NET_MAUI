using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;

namespace Jacquet_Valet_App.ViewModels
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly MovieInterface _movieInterface;

        // l'observablePorperty permet de notifier la vue lorsque la liste de films change
        
        [ObservableProperty]
        private ObservableCollection<MovieDto> movieList;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isErrorVisible;

        // constructeur qui injecte l'interface MovieInterface pour accéder aux données des films
        public MoviesViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            movieList = new ObservableCollection<MovieDto>();
            errorMessage = string.Empty;
        }

        // relayCommand permet de charger les films de manière asynchrone et de gérer les erreurs éventuelles
        [RelayCommand]
        private async Task LoadMovies()
        {
            IsErrorVisible = false;
            ErrorMessage = string.Empty;

            try
            {
                // on récupère la liste des films depuis l'interface MovieInterface et 
                // on les ajoute à l'observableCollection MovieList pour que la vue puisse les afficher
                var movies = await _movieInterface.GetAllMovies();

                if (movies != null)
                {
                    MovieList.Clear();
                    foreach (var movie in movies)
                    {
                        MovieList.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors du chargement des films : {ex.Message}";
                IsErrorVisible = true;
            }
        }

        // fonction pour naviguer vers la page de détail d'un film sélectionné, en passant l'id du film en paramètre de navigation
        [RelayCommand]
        private async Task GoToDetail(MovieDto movie)
        {
            if (movie == null)
                return;

            await Shell.Current.GoToAsync($"detail?id={movie.Id}");
        }
    }
}