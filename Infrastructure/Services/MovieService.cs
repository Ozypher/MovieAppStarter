using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    //Mapping into model data
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        // Call MovieRepository(call the database with dapper or EF core)
        var movies = _movieRepository.GetTop30RevenueMovies();

        var movieCards = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            movieCards.Add(new MovieCardModel
            {
                Id=movie.Id,PosterUrl = movie.PosterUrl,Title = movie.Title
            });
        }
        
        return movieCards;
    }
    
}