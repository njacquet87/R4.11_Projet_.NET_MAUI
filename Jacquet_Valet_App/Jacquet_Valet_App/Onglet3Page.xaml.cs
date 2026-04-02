using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jacquet_Valet_App.Models;
using Jacquet_Valet_App.Services;
using Jacquet_Valet_App.ViewModels;

namespace Jacquet_Valet_App;

public partial class Onglet3Page : ContentPage
{
    private readonly MoviesViewModel _viewModel;
    
    public Onglet3Page(MoviesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!MovieDataService.Movies.Any())
        {
            // Charger les films lorsque la page apparaît
            // pour éviter un que l'application crash si on essaie d'ajouter un film lorsque la collection est vide
            _viewModel.LoadMoviesCommand.Execute(null);
        }
    }

    private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                SelectedImage.Source = ImageSource.FromFile(localFilePath);
            }
        }
    }

    private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                
                SelectedImage.Source = ImageSource.FromFile(localFilePath);
            }
        }
    }

    private void OnAddMovieButtonClicked(object sender, EventArgs e)
    {
        // Pour pouvoir consulter la page de détail d'un film ajouté, il faut lui attribuer un id unique.
        // On prend le plus grand id de la collection partagée et on ajoute 1 pour le nouveau film
        var lastMovie = MovieDataService.Movies.OrderByDescending(m => m.Id).First();
        var newId = (lastMovie?.Id ?? 0) + 1;

        var newMovie = new MovieDto
        {
            Id = newId,
            Title = TitleEntry.Text,
            PosterURL = SelectedImage.Source is FileImageSource fileImageSource ? fileImageSource.File : null,
            ImdbId = ImdbIdEntry.Text
        };

        MovieDataService.Movies.Insert(0, newMovie);

        // Réinitialiser les champs
        TitleEntry.Text = string.Empty;
        ImdbIdEntry.Text = string.Empty;
        SelectedImage.Source = null;
    }
}