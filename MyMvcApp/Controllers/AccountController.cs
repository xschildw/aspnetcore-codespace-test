using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MyMvcApp.Models;
using MyMvcApp.Services;  // This is where your password service interface lives

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPasswordService _passwordService;
        private User _user;
        
        // The service is injected via the constructor.
        public AccountController(IPasswordService passwordService, User user)
        {
            _passwordService = passwordService;
            _user = user;
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
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.BirthYear = user.BirthYear;
                _user.UserName = user.UserName;
                _user.FavoriteColor = user.FavoriteColor;
                return RedirectToAction("PasswordSelect");
            }
            foreach(var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(_user);
        }
        
        // GET: /Account/PasswordSelect
        // Retrieves the stored user and displays a page where the password can be generated.
        [HttpGet]
        public IActionResult PasswordSelect()
        {
            var viewModel = new PasswordSelectViewModel
            {
                LastName = _user.LastName,
                BirthYear = _user.BirthYear,
                FavoriteColor = _user.FavoriteColor,
                Passwords = Enumerable.Range(0, 5)
                    .Select(i => _passwordService.GeneratePassword(_user.LastName, _user.BirthYear, _user.FavoriteColor))
                    .ToList()
            };

            return View(viewModel);
        }
        
        // POST: /Account/PasswordSelect
        // Calls the injected password service to generate a password based on user info.
        [HttpPost]
        public IActionResult PasswordSelect(PasswordSelectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _user.Password = viewModel.SelectedPassword;
            }
            return RedirectToAction("AccountInfo", _user);
        }
        
    }
}