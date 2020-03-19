using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private IPostService _postService;

        public ProfileController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string user)
        {
            IEnumerable<PostViewModel> posts = await _postService.GetAllByProfileAsync(user);
            return View(posts);
        }
    }
}