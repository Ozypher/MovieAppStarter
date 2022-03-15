using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<Review> GetReviewByUser(int movieId, int userId);
    Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
}