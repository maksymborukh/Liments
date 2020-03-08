using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Liments.MVC.Models;
using AutoMapper;
using Liments.MVC.Interfaces;
using Liments.MVC.Core.Entities;

namespace Liments.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService service)
        {
            _logger = logger;
            _userService = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userService.GetAllAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()).CreateMapper();
            var phones = mapper.Map<IEnumerable<User>, List<UserViewModel>>(users);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
