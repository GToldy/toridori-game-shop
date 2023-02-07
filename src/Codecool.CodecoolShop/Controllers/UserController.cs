using Codecool.CodecoolShop.Managers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Specialized;

namespace Codecool.CodecoolShop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDbManager _userDbManager;

        public UserController(UserDbManager userDbManager)
        {
            _userDbManager = userDbManager;
        }

        public IActionResult Login(User user)
        {
            return View();
        }

        public IActionResult Register(User user)
        {
            if (HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                _userDbManager.RegisterUser(user);
            }
            return View();
        }

        
    }
}
