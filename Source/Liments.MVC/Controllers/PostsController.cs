using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Controllers
{
    public class PostsController : Controller
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<PostViewModel> posts = await _postService.GetAllPublicAsync();
            return View(posts);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Like(string id)
        {
            //TODO check if possible to make async
            _postService.Like(User.Identity.Name, id);
            var posts = await _postService.GetAllPublicAsync();
            return PartialView("_Posts", posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(string id, string content)
        {
            _postService.AddComment(id, content, User.Identity.Name);
            var posts = await _postService.GetAllPublicAsync();
            return PartialView("_Posts", posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(string title, string content)
        {
            _postService.AddPost(content, title, User.Identity.Name);
            var posts = await _postService.GetAllByProfileAsync(User.Identity.Name);
            return PartialView("_Posts", posts);
        }
    }
}