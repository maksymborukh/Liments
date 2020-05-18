using Liments.MVC.Core.DBneo4j;
using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Liments.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private LimentsNeoContext _client;

        public AccountController(IUserService userService)
        {
            _userService = userService;
            _client = new LimentsNeoContext();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool hasAccess = await _userService.CheckCredentials(model.UserName, model.Password);

                if (hasAccess)
                {
                    await Authenticate(model.UserName);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isUserExists = await _userService.IsUserExist(model.Email, model.UserName);
                if (!isUserExists)
                {

                    if (model.Password == model.ConfirmPassword)
                    {
                        var user = new UserViewModel()
                        {
                            FirstName = string.Empty,
                            LastName = string.Empty,
                            Email = model.Email,
                            UserName = model.UserName,
                            Password = model.Password,
                            Subs = new List<string>(),
                            Fol = new List<string>()
                        };
                        await _userService.CreateAsync(user);
                        _client.Create(model.UserName);
                    }

                    await Authenticate(model.UserName);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Incorrect user name or password");
            }
            return View(model);
        }

        private async Task Authenticate(string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}