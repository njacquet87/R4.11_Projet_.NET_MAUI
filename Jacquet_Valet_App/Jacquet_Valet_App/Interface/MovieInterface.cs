using Jacquet_Valet_App.Models;
using Refit;

namespace Jacquet_Valet_App.Interface;

public interface MovieInterface
{
     // Récupérer tous les films classiques via l'endpoint GET /movies/classic
     [Get("/movies/classic")]
     Task<List<MovieDto>> GetAllMovies();
     
     // Récupérer un film classique par son ID via l'endpoint GET /movies/classic/{id}
     [Get("/movies/classic/{id}")]
     Task<MovieDto> GetMovieById(int id);
}