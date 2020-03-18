﻿using Liments.MVC.Interfaces;
using Liments.MVC.Models;
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

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<PostViewModel> posts = await _postService.GetAllPublicAsync();
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Like(string id)
        {
            //TODO check if possible to make async
            _postService.Like(User.Identity.Name, id);
            var posts = await _postService.GetAllPublicAsync();
            return PartialView("_Posts", posts);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string id, string content)
        {
            _postService.AddComment(id, content, User.Identity.Name);
            var posts = await _postService.GetAllPublicAsync();
            return PartialView("_Posts", posts);
        }
    }
}