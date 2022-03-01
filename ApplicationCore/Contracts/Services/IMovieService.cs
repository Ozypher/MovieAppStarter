using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    //business logic methods that relate to the movies
    List<MovieCardModel> GetTop30GrossingMovies();
}