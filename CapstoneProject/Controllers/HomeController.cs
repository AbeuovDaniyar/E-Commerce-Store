using CapstoneProject.Helper;
using CapstoneProject.Models;
using CapstoneProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISession userSession;
        UserService service = new UserService();

        /// <summary>
        /// Constructor that initializes logger, and userSession variables
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpContextAccessor"></param>
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this.userSession = httpContextAccessor.HttpContext.Session;
        }

        /// <summary>
        /// Index view controller method
        /// </summary>
        /// <returns>Index.cshtml</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// checks if user is logged in and redirects to Login View if user is not logged in
        /// and Index view if user is logged in
        /// </summary>
        /// <returns>Login.cshtml or Index.cshtml</returns>
        public IActionResult Login()
        {
            if (string.IsNullOrEmpty(this.userSession.GetString("userId")))
            {
                return View();
            }
            else 
            {
                return View("Index");
            }
        }

        /// <summary>
        /// Sets session variable for user and cart to null or empty
        /// </summary>
        /// <returns>Index.cshtml</returns>
        public IActionResult Logout()
        {
            Session.SetSessionToNull(HttpContext.Session, "cart");

            userSession.SetString("userId", "");
            userSession.SetString("userEmail", "");
            userSession.SetString("userRole", "");
            userSession.SetString("userName", "");

            return View("Index");
        }

        /// <summary>
        /// Authenticates user, checks if user credentials match creates user session and redirects to Index page,
        /// otherwise redirects to Login page
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Index.cshtml or Login.cshtml</returns>
        public IActionResult Authenticate(User user)
        {
            int result = service.authenticateUser(user.Email, user.Password);
            User currUser = service.getUserById(result);

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) 
            {
                return View("Login");
            }
            else if (string.IsNullOrEmpty(userSession.GetString("userId")) || 
                string.IsNullOrEmpty(userSession.GetString("userEmail")) ||
                string.IsNullOrEmpty(userSession.GetString("userRole")) ||
                string.IsNullOrEmpty(userSession.GetString("userName")))
            {
                if (result > -1)
                {
                    this.userSession.SetString("userId", currUser.Id.ToString());
                    this.userSession.SetString("userEmail", currUser.Email);
                    this.userSession.SetString("userRole", currUser.Role.ToString());
                    this.userSession.SetString("userName", currUser.FirstName);

                    return View("Index");
                }
                else 
                {                   
                    return View("Login");
                }
            }          
            else
            {
                return View("Login");
            }
        }

        /// <summary>
        /// returns Register View
        /// </summary>
        /// <returns>Register.cshtml</returns>
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ProcessRegister(User user)
        {
            if (service.insertUser(user) > -1)
            {
                return View("Login");
            }
            else
            {
                return View("Register");
            }

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
