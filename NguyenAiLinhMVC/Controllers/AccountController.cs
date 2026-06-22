using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [Route("[controller]")]
    [Route("Accounts")]
    [SessionAuthorize("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? search)
        {
            var items = await _accountService.GetAllAsync(search);
            ViewBag.Search = search;
            return View(items.Select(MapAccount).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AccountFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMessage("Invalid account data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _accountService.CreateAsync(new SaveSystemAccountRequestDto
            {
                AccountName = model.AccountName,
                AccountEmail = model.AccountEmail,
                AccountRole = model.AccountRole,
                AccountPassword = model.AccountPassword
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(AccountFormViewModel model)
        {
            if (!ModelState.IsValid || !model.AccountId.HasValue)
            {
                SetMessage("Invalid account data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _accountService.UpdateAsync(new SaveSystemAccountRequestDto
            {
                AccountId = model.AccountId,
                AccountName = model.AccountName,
                AccountEmail = model.AccountEmail,
                AccountRole = model.AccountRole,
                AccountPassword = model.AccountPassword
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(short accountId)
        {
            var result = await _accountService.DeleteAsync(accountId);
            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        private void SetMessage(string message, bool isSuccess)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = isSuccess ? "success" : "danger";
        }

        private static AccountFormViewModel MapAccount(SystemAccountDto item)
        {
            return new AccountFormViewModel
            {
                AccountId = item.AccountId,
                AccountName = item.AccountName,
                AccountEmail = item.AccountEmail,
                AccountRole = item.AccountRole,
                RoleName = item.RoleName,
                CreatedNewsCount = item.CreatedNewsCount,
                AccountPassword = string.Empty
            };
        }
    }
}
