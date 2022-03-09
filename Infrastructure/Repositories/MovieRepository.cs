using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : EfRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
    {
        // EF Core or Dapper
        // they provide both sync and async methods in those libraries
        // get top 30 movies by revenue
        var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }

    public async Task<PagedResultSet<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        //get totalmovies count for that genre
        var totalMoviesCountByGenre = await _dbContext.MovieGenres.Where(m => m.GenreId == genreId).CountAsync();

        if (totalMoviesCountByGenre == 0)
        {
            throw new Exception("No movies found.");
        }

        var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(g => g.Movie)
            .OrderBy(g => g.MovieId)
            .Select(m=>new Movie 
            {
                Id=m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title
            })
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCountByGenre);
        return pagedMovies;
    }

    public override async Task<Movie> GetById(int id)
    {
        // First throw ex if no matches found
        // FirstOrDefault safest
        // Single throw ex 0 or more than 1
        // SingleOrDefault throw ex if more than 1 
        // we need to use Include method
        var movieDetails = await _dbContext.Movies.Include(m => m.Genres).ThenInclude(m => m.Genre)
            .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefaultAsync( m=> m.Id == id);
        return movieDetails;
    }
}