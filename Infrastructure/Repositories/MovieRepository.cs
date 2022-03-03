using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class MovieRepository : EfRepository<Movie>, IMovieRepository, IMovieService
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Movie> GetTop30RevenueMovies()
    {
        //Get top 30 Movies by Revenue
        var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30);
        return movies;
    }

    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        throw new NotImplementedException();
    }
}