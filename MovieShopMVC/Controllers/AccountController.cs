using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // account/register => GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // save the password and account info with salt
                var user = await _accountService.CreateUser(model);
                return RedirectToAction("Login");
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var userLoggedIn = await _accountService.ValidateUser(model.Email, model.Password);
            if (userLoggedIn != null)
            {
                //create auth cookie and store info
                //user information is called claims
                
                //create claim object to store user claims info
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userLoggedIn.Email),
                    new Claim(ClaimTypes.NameIdentifier, userLoggedIn.Id.ToString()),
                    new Claim(ClaimTypes.GivenName, userLoggedIn.FirstName),
                    new Claim(ClaimTypes.Surname, userLoggedIn.LastName),
                    new Claim(ClaimTypes.DateOfBirth, userLoggedIn.DateOfBirth.ToShortDateString()),
                    new Claim("FullName", userLoggedIn.FirstName + ","+userLoggedIn.LastName),
                    new Claim("Language","en")
                };
                
                //ident object
                var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                
                //Create cookie
                //Signinasync

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                
                return LocalRedirect("~/");
            }
            else
            {
                return View();
            }
        }
    }
}