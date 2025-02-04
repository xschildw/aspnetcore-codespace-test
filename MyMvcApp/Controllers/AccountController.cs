using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MyMvcApp.Models;
using MyMvcApp.Services;  // This is where your password service interface lives

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPasswordService _passwordService;
        
        // The service is injected via the constructor.
        public AccountController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }
        
        // GET: /Account/AccountInfo
        // Displays a form for the user to enter basic account details.
        [HttpGet]
        public IActionResult AccountInfo()
        {
            return View();
        }
        
        // POST: /Account/AccountInfo
        // Receives the user details, validates, and then stores them temporarily.
        [HttpPost]
        public IActionResult AccountInfo(User user)
        {
            if (ModelState.IsValid)
            {
                // Serialize the user object and store it in TempData.
                TempData["User"] = JsonConvert.SerializeObject(user);
                return RedirectToAction("PasswordSelect");
            }
            foreach(var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(user);
        }
        
        // GET: /Account/PasswordSelect
        // Retrieves the stored user and displays a page where the password can be generated.
        [HttpGet]
        public IActionResult PasswordSelect()
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                // Keep the TempData for the next request.
                TempData.Keep("User");
                var viewModel = new PasswordSelectViewModel
                {
                    LastName = user.LastName,
                    BirthYear = user.BirthYear,
                    FavoriteColor = user.FavoriteColor,
                    Passwords = new List<String>{
                        _passwordService.GeneratePassword(user.LastName, user.BirthYear, user.FavoriteColor)
                    }
                };

                return View(viewModel);
            }
            return RedirectToAction("AccountInfo");
        }
        
        // POST: /Account/PasswordSelect
        // Calls the injected password service to generate a password based on user info.
        [HttpPost]
        public IActionResult PasswordSelect(PasswordSelectViewModel viewModel)
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                // Keep the TempData for the next request.
                TempData.Keep("User");

                if (ModelState.IsValid)
                {
                    user.Password = viewModel.SelectedPassword;
                    return RedirectToAction("ConfirmAccount", user);
                }
            }
            return View(viewModel);
        }
/*        
        // GET: /Account/ConfirmAccount
        // Displays a confirmation page with the user details.
        [HttpGet]
        public IActionResult ConfirmAccount()
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                TempData.Keep("User");
                return View(user);
            }
            return RedirectToAction("AccountInfo");
        }
        
        // GET: /Account/LoginPage
        // Displays the login form with the username (and optionally password) prefilled.
        [HttpGet]
        public IActionResult LoginPage()
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                TempData.Keep("User");
                return View(user);
            }
            return RedirectToAction("AccountInfo");
        }
        
        // POST: /Account/LoginPage
        // Processes the login form submission.
        [HttpPost]
        public IActionResult LoginPage(string username, string password)
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                // A simple (and not secure) login check.
                if (username == user.UserName && password == user.Password)
                {
                    return RedirectToAction("LoginSuccess");
                }
            }
            
            ModelState.AddModelError("", "Invalid credentials");
            return View();
        }
        
        // GET: /Account/LoginSuccess
        // Displays a simple success message after a successful login.
        [HttpGet]
        public IActionResult LoginSuccess()
        {
            return View();
        }
*/
    }
}