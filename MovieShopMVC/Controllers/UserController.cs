using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers;
[Authorize]

public class UserController : Controller
{
    private readonly ICurrentUser _currentUser;
    
    public UserController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        //get the user id from cookies/claims
        var userId = _currentUser.UserId;
        //sent the userid to userservice and retreive the movies
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