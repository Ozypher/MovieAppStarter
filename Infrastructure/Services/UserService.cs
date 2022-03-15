using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        var user = await _userRepository.GetById(userId);
        user.Purchases.Add
        (new Purchase
        {
            MovieId = purchaseRequest.MovieId, PurchaseNumber = purchaseRequest.PurchaseNumber, TotalPrice = purchaseRequest.TotalMoney, PurchaseDateTime = purchaseRequest.PurchaseDateTime
        });
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        var user = await _userRepository.GetById(userId);
        if (user.Purchases.FirstOrDefault(u => u.MovieId == purchaseRequest.MovieId)!=null)
        {
            return true;
        }

        return false;

    }

    public async Task<List<PurchaseRequestModel>> GetAllPurchasesForUser(int id)
    {
        var user = await _userRepository.GetById(id);
        List<PurchaseRequestModel> purchases = new List<PurchaseRequestModel>();
        foreach (var movie in purchases)
        {
            purchases.Add(new PurchaseRequestModel
            {
                MovieId = movie.MovieId, PurchaseNumber = movie.PurchaseNumber, TotalMoney = movie.TotalMoney, PurchaseDateTime = movie.PurchaseDateTime
            });
        }

        return purchases;

    }

    public async Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId)
    {
        var user = await _userRepository.GetById(userId);
        var purchase = user.Purchases.FirstOrDefault(u => u.MovieId == movieId);
        if (purchase.Equals(null))
        {
            throw new Exception("User does not have purchase on account.");
        }
        else
        {
            return new PurchaseRequestModel
            {
                MovieId = purchase.MovieId, PurchaseNumber = purchase.PurchaseNumber, TotalMoney = purchase.TotalPrice, PurchaseDateTime = purchase.PurchaseDateTime
            };
        }
        
    }

    public async Task<FavoriteRequestModel> AddFavorite(FavoriteRequestModel favoriteRequestModel)
    {
        var user = await _userRepository.GetById(favoriteRequestModel.UserId);
        if (!FavoriteExists(favoriteRequestModel.UserId,favoriteRequestModel.MovieId).Result)
        {
            user.Favorites.Add(new Favorite
            {
                MovieId = favoriteRequestModel.MovieId, UserId = user.Id
            });
        }
        else
        {
            throw new Exception("Already Favorite");
        }

        var favorite = new FavoriteRequestModel
        {
            UserId = user.Id, MovieId = favoriteRequestModel.MovieId
        };
        return favorite;
    }

    public async Task<FavoriteRequestModel> RemoveFavorite(FavoriteRequestModel favoriteRequest)
    {
        var user = await _userRepository.GetById(favoriteRequest.UserId);
        if (!FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId).Result)
        {
            throw new Exception("Not a favorite");
        }
        else
        {
            user.Favorites.Remove(new Favorite
            {
                MovieId = favoriteRequest.MovieId, UserId = favoriteRequest.UserId
            });
        }

        return new FavoriteRequestModel
        {
            MovieId = favoriteRequest.MovieId, UserId = favoriteRequest.UserId
        };
    }

    public async Task<bool> FavoriteExists(int id, int movieId)
    {
        var user = await _userRepository.GetById(id);
        if (!user.Favorites.FirstOrDefault(x => x.MovieId == movieId).Equals(null))
        {
            return true;
        }

        return false;
    }

    public async Task<List<FavoriteRequestModel>> GetAllFavoritesForUser(int id)
    {
        var user = await _userRepository.GetById(id);
        List<FavoriteRequestModel> returnlist = new List<FavoriteRequestModel>();
        foreach (var favorite in user.Favorites)
        {
            returnlist.Add(new FavoriteRequestModel{
                MovieId = favorite.MovieId, UserId = favorite.UserId
            });
        }

        return returnlist;
    }

    public async Task AddMovieReview(ReviewRequestModel reviewRequest)
    {
        var user = await _userRepository.GetById(reviewRequest.UserId);
        user.Reviews.Add(new Review
        {
            MovieId = reviewRequest.MovieId, UserId = reviewRequest.UserId, Rating = reviewRequest.Rating, ReviewText = reviewRequest.ReviewText
        });
    }

    public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
    {
        var user = await _userRepository.GetById(reviewRequest.UserId);
        var item = user.Reviews.FirstOrDefault(x => x.MovieId == reviewRequest.MovieId);
        user.Reviews.Remove(item);
        user.Reviews.Add(new Review
        {
            MovieId = reviewRequest.MovieId, UserId = reviewRequest.UserId, Rating = reviewRequest.Rating, ReviewText = reviewRequest.ReviewText
        });
    }

    public async Task DeleteMovieReview(int userId, int movieId)
    {
        var user = await _userRepository.GetById(userId);
        var item = user.Reviews.FirstOrDefault(x => x.MovieId == movieId);
        user.Reviews.Remove(item);
    }

    public async Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id)
    {
        var user = await _userRepository.GetById(id);
        List<ReviewRequestModel> reviewlist = new List<ReviewRequestModel>();
        foreach (var review in user.Reviews)
        {
            reviewlist.Add(new ReviewRequestModel
            {
                MovieId = review.MovieId, Rating = review.Rating, ReviewText = review.ReviewText, UserId = review.UserId
            });
        }

        return reviewlist;
    }
}