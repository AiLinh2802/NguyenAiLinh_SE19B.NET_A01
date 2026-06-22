using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [HttpGet("/")]
        [Route("/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Account/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.LoginAsync(model.Email, model.Password);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString(SessionKeys.UserEmail, result.Email);
            HttpContext.Session.SetString(SessionKeys.UserRole, result.Role);
            HttpContext.Session.SetString(SessionKeys.AccountName, result.AccountName);

            if (result.AccountId.HasValue)
            {
                HttpContext.Session.SetInt32(SessionKeys.AccountId, result.AccountId.Value);
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("/Account/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Account/Login");
        }
    }
}
