using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        // Call MovieRepository(call the database with dapper or EF core)
        // dummy data for now

        var movies = new List<MovieCardModel>()
        {
            new MovieCardModel {Id = 1, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "Inception"},
            new MovieCardModel {Id = 2, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "a"},
            new MovieCardModel {Id = 3, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "b"},
            new MovieCardModel {Id = 4, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "c"},
            new MovieCardModel {Id = 5, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "d"},
            new MovieCardModel {Id = 6, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "e"},
            new MovieCardModel {Id = 7, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "f"},
            new MovieCardModel {Id = 8, PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm3426651392/?ref_=tt_ov_i", Title = "g"}
        };
        return movies;
    }
}