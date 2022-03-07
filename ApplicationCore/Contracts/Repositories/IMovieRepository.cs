using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetTop30RevenueMovies();
    //total count, page size, pageNumber current, total pages
    // PagedModel=>
    // Tuple 
    //Task<(IEnumerable<Movie>,int totalCount,int totalPages)> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1);
    Task<PagedResultSet<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1);
}