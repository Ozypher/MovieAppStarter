using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async  Task<IActionResult>  Details(int id)
        {
            // Movie Service with Details
            // pass the movie details data to view
            // Data
            // Remote Database 

            // CPU bound operation => PI => Loan callcuator, image pro
            // I/O bound operation => database calls, file, images, videos

            // Network speed, SQL Server => Query , Server Memory
            // T1 is just waiting
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Genres(int id, int pagesize = 30, int pagenumber = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pagesize, pagenumber);
            return View(pagedMovies);
        }
    }
}