using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Liments.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private IPostService _postService;
        private IUserService _userService;

        public ProfileController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IndexAsync(string user)
        {
            ProfileViewModel profile = new ProfileViewModel();
            profile.Posts = await _postService.GetAllByProfileAsync(user);
            profile.Profile = await _userService.GetByUserNameAsync(user);
            return View(profile);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Follow(string user)
        {
            await _userService.FollowAsync(User.Identity.Name, user);

            var data = await _userService.GetByUserNameAsync(user);
            return PartialView("_ProfileData", data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Unfollow(string user)
        {
            await _userService.UnfollowAsync(User.Identity.Name, user);

            var data = await _userService.GetByUserNameAsync(user);
            return PartialView("_ProfileData", data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Unfollow2(string user)
        {
            await _userService.UnfollowAsync(User.Identity.Name, user);

            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(string user)
        {
            await _userService.UnfollowAsync(user, User.Identity.Name);

            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userService.GetByUserNameAsync(User.Identity.Name);

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, string fname, string lname, string email, string username, string pass)
        {            
            try
            {
                await _userService.UpdateAsync(id, fname, lname, email, username, pass);

                if (User.Identity.Name != username && username != null)
                {
                    await ReLogin(username);
                    return RedirectToAction("EditProfile", "Profile");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Username exists")
                {
                    ModelState.AddModelError("UserName", "Username exists");
                }
                if (ex.Message == "Email exists")
                {
                    ModelState.AddModelError("Email", "Email exists");
                }
            }
            
            var user = await _userService.GetByIdAsync(id);

            return View("EditProfile", user);
        }

        [Authorize]
        private async Task ReLogin(string username)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}