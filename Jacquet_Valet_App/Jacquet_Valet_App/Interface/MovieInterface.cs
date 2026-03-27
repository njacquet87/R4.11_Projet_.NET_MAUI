using Jacquet_Valet_App.Models;
using Refit;

namespace Jacquet_Valet_App.Interface;

public interface MovieInterface
{
     [Get("/movies/classic")]
     Task<List<MovieDto>> GetAllMovies();
     
     [Get("/movies/classic/{id}")]
     Task<MovieDto> GetMovieById(int id);
}