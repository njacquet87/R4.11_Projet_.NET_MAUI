using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jacquet_Valet_App.Models;
using Jacquet_Valet_App.Services;

namespace Jacquet_Valet_App;

public partial class Onglet3Page : ContentPage
{
    public Onglet3Page()
    {
        InitializeComponent();
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
        var newMovie = new MovieDto
        {
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