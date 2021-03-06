using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _MovieService;
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;


        public MoviesController(IMovieService movieService, ICurrentUser currentUser, IUserService userService)
        {
            _MovieService = movieService;
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["isPurchased"] = false;
            ViewData["isFavorited"] = false;
            if (_currentUser.IsAuthenticated)
            {
                var currentUser = _currentUser.UserId;
                var getMoviesPurchasesDetails = await _userService.GetPurchasesDetails(currentUser, id);
                if (getMoviesPurchasesDetails != null)
                {
                    ViewData["isPurchased"] = true;
                }

                var isFavorited = await _userService.FavoriteExists(currentUser, id);
                if (isFavorited)
                {
                    ViewData["isFavorited"] = true;
                }
            }
            var movieDetails = await _MovieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedNumbers = await _MovieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            return View("PagedMovies", pagedNumbers);
        }
    }
}