using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieServiceMock : IMovieService
{
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        // Call MovieRepository(call the database with dapper or EF core)
        // dummy data for now

        var movies = new List<MovieCardModel>()
        {
            new MovieCardModel {Id = 1, PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_FMjpg_UX700_.jpg", Title = "Inception"},
            new MovieCardModel {Id = 2, PosterUrl = "https://images.squarespace-cdn.com/content/v1/5acd17597c93273e08da4786/1547848016060-286BK7E4Y0THATD46Z7A/SHREK+Close.png", Title = "Shrek"},
            new MovieCardModel {Id = 3, PosterUrl = "https://m.media-amazon.com/images/M/MV5BNTg3YTI1ZmUtMjM4MC00MzVkLTg2ODItMTczNjc3MzI2NjU0XkEyXkFqcGdeQXVyMTAyOTE2ODg0._V1_FMjpg_UX681_.jpg", Title = "Kingsman"}
        };
        return movies;
    }
}