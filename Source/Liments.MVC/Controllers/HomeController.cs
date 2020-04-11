using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Liments.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<PostViewModel> posts = await _postService.GetAllFolPostAsync(User.Identity.Name);
            return View(posts);
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}