using Data.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;
using Service.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region Profile
        [HttpGet]
        public async Task<IActionResult>  Profile()
        {
            var IsSignIn = _appUserService.IsSignIn();
            if (IsSignIn)
            {
                var user = await _appUserService.GetUserDTO();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult UpdateProfile(UserDTO user)
        {
            var IsSignIn = _appUserService.IsSignIn();
            if (IsSignIn)
            {
                var result = _appUserService.UpdateProfile(user).Result;
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            var IsSignIn = _appUserService.IsSignIn();
            return IsSignIn?  RedirectToAction("Index", "Home"): View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO model)
        {
            var result = await _appUserService.Register(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            var IsSignIn = _appUserService.IsSignIn();
            if (IsSignIn) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDTO model)
        {
            var result = await _appUserService.Login(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _appUserService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}
