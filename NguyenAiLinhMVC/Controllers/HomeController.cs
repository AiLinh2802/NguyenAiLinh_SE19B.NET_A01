using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Models;
using NguyenAiLinhMVC.Infrastructure;
using System.Diagnostics;

namespace NguyenAiLinhMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public HomeController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionKeys.UserRole)))
            {
                return RedirectToAction(nameof(PublicNews));
            }

            return View();
        }

        [HttpGet("/News")]
        [HttpGet("/News/Public")]
        public async Task<IActionResult> PublicNews(string? search)
        {
            var items = await _newsArticleService.GetPublicNewsAsync(search);
            ViewBag.Search = search;
            return View(items);
        }

        public async Task<IActionResult> NewsDetails(string id)
        {
            var item = await _newsArticleService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var role = HttpContext.Session.GetString(SessionKeys.UserRole);
            var canViewInactive = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase) ||
                                  string.Equals(role, "Staff", StringComparison.OrdinalIgnoreCase);

            if (!item.NewsStatus && !canViewInactive)
            {
                return RedirectToAction(nameof(PublicNews));
            }

            return View(item);
        }

        [HttpGet("/AccessDenied")]
        public IActionResult AccessDenied()
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
