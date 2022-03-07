using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public override Cast GetById(int id)
    {
        var castDetails = _dbContext.Casts.Include(c => c.MovieCasts).
            ThenInclude(c=>c.Movie).FirstOrDefault(c => c.Id == id);
        return castDetails;
    }

    public CastDetailsModel GetCastDetails(int id)
    {
        throw new NotImplementedException();
    }
}