using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [Route("[controller]")]
    [SessionAuthorize("Staff")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var accountId = HttpContext.Session.GetInt32(SessionKeys.AccountId);
            if (!accountId.HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }

            var profile = await _profileService.GetByIdAsync((short)accountId.Value);
            if (profile == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(new ProfileFormViewModel
            {
                AccountId = profile.AccountId,
                AccountName = profile.AccountName,
                AccountEmail = profile.AccountEmail,
                AccountPassword = string.Empty,
                RoleName = profile.RoleName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid profile data.";
                TempData["MessageType"] = "danger";
                return RedirectToAction(nameof(Index));
            }

            var result = await _profileService.UpdateProfileAsync(new SaveSystemAccountRequestDto
            {
                AccountId = model.AccountId,
                AccountName = model.AccountName,
                AccountEmail = model.AccountEmail,
                AccountPassword = model.AccountPassword
            });

            if (result.IsSuccess)
            {
                HttpContext.Session.SetString(SessionKeys.AccountName, model.AccountName);
                HttpContext.Session.SetString(SessionKeys.UserEmail, model.AccountEmail);
            }

            TempData["Message"] = result.Message;
            TempData["MessageType"] = result.IsSuccess ? "success" : "danger";
            return RedirectToAction(nameof(Index));
        }
    }
}
