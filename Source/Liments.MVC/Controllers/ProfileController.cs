using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}