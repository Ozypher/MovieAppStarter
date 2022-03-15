using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
    Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
    Task<List<PurchaseRequestModel>> GetAllPurchasesForUser(int id);
    Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId);
    Task<FavoriteRequestModel> AddFavorite(FavoriteRequestModel favoriteRequest);
    Task<FavoriteRequestModel> RemoveFavorite(FavoriteRequestModel favoriteRequest);
    Task<bool> FavoriteExists(int id, int movieId);
    Task<List<FavoriteRequestModel>> GetAllFavoritesForUser(int id);
    Task AddMovieReview(ReviewRequestModel reviewRequest);
    Task UpdateMovieReview(ReviewRequestModel reviewRequest);
    Task DeleteMovieReview(int userId, int movieId);
    Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id);
}