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
        
        [ObservableProperty]
        private int nbFilmsAVoir;
        
        [ObservableProperty]
        private int nbFilmsVus;

        [ObservableProperty]
        private bool isNoMoviesFound;

        // constructeur qui injecte l'interface MovieInterface pour accéder aux données des films
        public MoviesViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            MovieList = MovieDataService.Movies; // Utiliser la collection partagée
            FilmsAVoir = MovieDataService.FilmsAVoir;
            FilmsVus = MovieDataService.FilmsVus;
            errorMessage = string.Empty;
            IsErrorVisible = false;
            NbFilmsAVoir = FilmsAVoir.Count;
            NbFilmsVus = FilmsVus.Count;

            // Abonnement aux événements de changement de collection pour mettre à jour les compteurs
            FilmsAVoir.CollectionChanged += (s, e) => NbFilmsAVoir = FilmsAVoir.Count;
            FilmsVus.CollectionChanged += (s, e) => NbFilmsVus = FilmsVus.Count;
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

        [RelayCommand]
        private void RemoveFromFilmsAVoir(MovieDto movie)
        {
            if (movie != null)
            {
                FilmsAVoir.Remove(movie);
            }
        }

        [RelayCommand]
        private void RemoveFromFilmsVus(MovieDto movie)
        {
            if (movie != null)
            {
                FilmsVus.Remove(movie);
            }
        }
        
        [RelayCommand]
        private void Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Si le texte de recherche est vide, afficher tous les films
                MovieList = new ObservableCollection<MovieDto>(MovieDataService.Movies);
                IsNoMoviesFound = false;
            }
            else
            {
                // Filtrer les films en fonction du texte de recherche (par titre)
                var filteredMovies = MovieDataService.Movies
                    .Where(m => m.Title != null && m.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                MovieList = new ObservableCollection<MovieDto>(filteredMovies);
                IsNoMoviesFound = filteredMovies.Count == 0;
            }
        }
    }
}