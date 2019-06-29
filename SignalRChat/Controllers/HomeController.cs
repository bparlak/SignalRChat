using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Models;
using Microsoft.AspNetCore.Http;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            string userName = model.Username;
            if (!String.IsNullOrEmpty(userName))
            {
                UserRepository.AddUser(userName);
                return RedirectToAction("ChatRooms",new {userName});
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ChatRooms(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login");
            }
            ChatRoomsViewModel model = new ChatRoomsViewModel();
            model.UserList = UserRepository.UserList;
            model.Username = userName;
            return View(model);
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
