using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    IEnumerable<Movie> GetTop30RevenueMovies();
}