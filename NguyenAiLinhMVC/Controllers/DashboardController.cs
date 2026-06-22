using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [SessionAuthorize("Admin", "Staff", "Lecturer")]
    public class DashboardController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;

        public DashboardController(
            IAccountService accountService,
            ICategoryService categoryService,
            INewsArticleService newsArticleService)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _newsArticleService = newsArticleService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString(SessionKeys.UserRole) ?? string.Empty;
            var model = new DashboardViewModel
            {
                Role = role,
                AccountName = HttpContext.Session.GetString(SessionKeys.AccountName) ?? string.Empty
            };

            if (role == "Admin")
            {
                var accounts = await _accountService.GetAllAsync();
                var categories = await _categoryService.GetAllAsync();
                var newsArticles = await _newsArticleService.GetAllAsync();

                model.TotalAccounts = accounts.Count;
                model.TotalCategories = categories.Count;
                model.TotalNewsArticles = newsArticles.Count;
                model.TotalActiveNewsArticles = newsArticles.Count(article => article.NewsStatus);
                model.LatestNewsArticles = newsArticles
                    .OrderByDescending(article => article.CreatedDate)
                    .Take(5)
                    .ToList();

                return View(model);
            }

            if (role == "Staff")
            {
                var accountId = HttpContext.Session.GetInt32(SessionKeys.AccountId);
                if (!accountId.HasValue)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var myNewsArticles = await _newsArticleService.GetByCreatorIdAsync((short)accountId.Value);

                model.MyTotalNewsArticles = myNewsArticles.Count;
                model.MyActiveNewsArticles = myNewsArticles.Count(article => article.NewsStatus);
                model.MyInactiveNewsArticles = myNewsArticles.Count(article => !article.NewsStatus);
                model.LatestNewsArticles = myNewsArticles
                    .OrderByDescending(article => article.CreatedDate)
                    .Take(5)
                    .ToList();

                return View(model);
            }

            return RedirectToAction("PublicNews", "Home");
        }
    }
}
