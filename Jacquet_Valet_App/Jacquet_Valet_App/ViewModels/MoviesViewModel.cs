using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;
using Jacquet_Valet_App.Services;

namespace Jacquet_Valet_App.ViewModels
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly MovieInterface _movieInterface;

        // l'observablePorperty permet de notifier la vue lorsque la liste de films change
        
        [ObservableProperty]
        private ObservableCollection<MovieDto> movieList;

        [ObservableProperty]
        private ObservableCollection<MovieDto> filmsAVoir;

        [ObservableProperty]
        private ObservableCollection<MovieDto> filmsVus;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isErrorVisible;

        // constructeur qui injecte l'interface MovieInterface pour accéder aux données des films
        public MoviesViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            MovieList = MovieDataService.Movies; // Utiliser la collection partagée
            FilmsAVoir = MovieDataService.FilmsAVoir;
            FilmsVus = MovieDataService.FilmsVus;
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
                var movies = await _movieInterface.GetAllMovies();
                if (movies != null)
                {
                    // Ajoute uniquement les films qui ne sont pas déjà dans la liste
                    foreach (var movie in movies)
                    {
                        if (!MovieList.Any(m => m.ImdbId == movie.ImdbId))
                        {
                            MovieList.Add(movie);
                        }
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