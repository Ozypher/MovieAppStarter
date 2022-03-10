using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class UserController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        // check if logged in
        var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;

        if (isAuthenticated)
        {
            //get the user id from cookies/claims
            var userId = Convert.ToInt32( HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //sent the userid to userservice and retreive the movies
        }
        //get user id
        //send user id to db to get movies
        //cookie based authentication
        
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Reviews()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> BuyMovie()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> FavoriteMovie()
    {
        return View();
    }
    
}