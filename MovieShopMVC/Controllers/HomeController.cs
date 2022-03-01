using System.Diagnostics;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // notes
        // our controllers are very thin/lean
        // most logic should come from other dependencies, such as services
        // Interfaces
        // void method(int x, IMovieService service);
        // class MovieService: IMovieService{};

        // var movieservice = new MovieService();

        // method(20,movieservice);

        var movieService = new MovieService();
        var movies = movieService.GetTop30GrossingMovies();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}