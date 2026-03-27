using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Jacquet_Valet_App.Interface;
using Jacquet_Valet_App.Models;

namespace Jacquet_Valet_App.ViewModels
{
    public partial class Onglet2ViewModel : ObservableObject
    {
        private readonly MovieInterface _movieInterface;
        private bool _isLoading = false;

        [ObservableProperty]
        private ObservableCollection<Jacquet_Valet_App.Models.MovieDto> movieList;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isErrorVisible;

        [ObservableProperty]
        private bool isLoadingVisible;

        public Onglet2ViewModel(MovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
            movieList = new ObservableCollection<Jacquet_Valet_App.Models.MovieDto>();
            errorMessage = string.Empty;
            isErrorVisible = false;
            isLoadingVisible = false;
        }

        public async Task LoadMovies()
        {
            // Éviter les appels multiples simultanés
            if (_isLoading)
                return;

            try
            {
                _isLoading = true;
                IsErrorVisible = false;
                ErrorMessage = string.Empty;
                IsLoadingVisible = true;

                var movies = await _movieInterface.GetAllMovies();

                if (movies != null && movies.Count > 0)
                {
                    MovieList.Clear();
                    foreach (var movie in movies)
                    {
                        MovieList.Add(movie);
                    }

                    Console.WriteLine($"Films chargés: {MovieList.Count}");
                }
                
                IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des films: {ex.Message}");
                ErrorMessage = $"Erreur : {ex.Message}";
                IsErrorVisible = true;
                IsLoadingVisible = false;
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}

