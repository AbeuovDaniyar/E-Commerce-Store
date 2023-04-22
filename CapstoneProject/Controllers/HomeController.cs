/* This is a C# code for a controller class named `HomeController` in a web application. It includes
several methods that handle HTTP requests and responses for different views such as `Index`,
`Login`, `Logout`, `Authenticate`, `Register`, `ProcessRegister`, `Privacy`, and `Error`. */
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
    /* The HomeController class contains methods for user authentication, registration, and session
    management in a C# web application. */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISession userSession;
        UserService service = new UserService();

        
        /* This is the constructor method for the `HomeController` class. It takes in two parameters:
        `ILogger<HomeController> logger` and `IHttpContextAccessor httpContextAccessor`. */
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this.userSession = httpContextAccessor.HttpContext.Session;
        }

        
        /// The function returns a view for the Index page.
        /// 
        /// @return The `Index` action method is returning a `ViewResult` object, which represents a
        /// view that will be rendered as the response to the client's request.
        public IActionResult Index()
        {
            return View();
        }

        
        /// The Login function checks if a user is already logged in and returns the appropriate view.
        /// 
        /// @return If the "userId" value in the userSession is null or empty, the Login view is
        /// returned. Otherwise, the Index view is returned.
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

        
        /// The function logs out the user and clears their session data.
        /// 
        /// @return The method is returning a View named "Index".
        public IActionResult Logout()
        {
            Session.SetSessionToNull(HttpContext.Session, "cart");

            userSession.SetString("userId", "");
            userSession.SetString("userEmail", "");
            userSession.SetString("userRole", "");
            userSession.SetString("userName", "");

            return View("Index");
        }

        
        /// This function authenticates a user and sets session variables if the authentication is
        /// successful.
        /// 
        /// @param User The User class is a model class that represents a user in the system. It
        /// contains properties such as Email, Password, Id, FirstName, and Role.
        /// 
        /// @return The method returns an IActionResult, which could be a ViewResult or a RedirectResult
        /// depending on the conditions met in the if-else statements.
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

        
        /// This function returns a view for user registration in a C# web application.
        /// 
        /// @return The method is returning a View result.
        public IActionResult Register()
        {
            return View();
        }

        /// The function processes user registration and returns a view for either login or registration
        /// based on the success of inserting the user.
        /// 
        /// @param User The User parameter is an object that represents a user in the system. It likely
        /// contains properties such as username, password, email, and other relevant information. The
        /// method is using this object to register a new user in the system by passing it to a service
        /// method called insertUser. If the user is
        /// 
        /// @return The method is returning a View result, either "Login" or "Register" depending on the
        /// result of the insertUser method.
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

        /// This function returns a view for the privacy policy.
        /// 
        /// @return A View result is being returned.
        public IActionResult Privacy()
        {
            return View();
        }

        /// This function returns an error view with a unique request ID or trace identifier.
        /// 
        /// @return An IActionResult object that returns a view with an ErrorViewModel object as its
        /// model. The ErrorViewModel object contains a RequestId property that is set to either the
        /// current activity's Id or the HttpContext's TraceIdentifier.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
