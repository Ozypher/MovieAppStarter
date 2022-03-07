using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class CastController : Controller
{
    private readonly ICastService _castService;
    
    public CastController(ICastService castService)
    {
        _castService = castService;
    }

    public async Task<IActionResult> Details(int id)
    {
        // Movie Service with Details
        // pass the movie details data to view
        var castDetails = await _castService.GetCastDetails(id);
        return View(castDetails);
    }
}