using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.DAOs;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [Route("NewsArticles")]
    [SessionAuthorize("Staff")]
    public class NewsController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public NewsController(
            INewsArticleService newsArticleService,
            ICategoryService categoryService,
            ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? search)
        {
            var items = await _newsArticleService.GetAllAsync(search);
            await SetLookupDataAsync();
            ViewBag.NextNewsArticleId = await _newsArticleService.GetNextIdAsync();
            ViewBag.Search = search;
            return View(items.Select(MapNews).ToList());
        }

        [HttpGet("History")]
        [HttpGet("/NewsHistory")]
        public async Task<IActionResult> History(string? search)
        {
            var accountId = HttpContext.Session.GetInt32(SessionKeys.AccountId);
            if (!accountId.HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }

            var items = await _newsArticleService.GetByCreatorIdAsync((short)accountId.Value, search);
            ViewBag.Search = search;
            return View(items.Select(MapNews).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(NewsArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMessage("Invalid news article data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _newsArticleService.CreateAsync(new SaveNewsArticleRequestDto
            {
                NewsArticleId = model.NewsArticleId,
                NewsTitle = model.NewsTitle,
                Headline = model.Headline,
                NewsContent = model.NewsContent,
                NewsSource = model.NewsSource,
                CategoryId = model.CategoryId,
                NewsStatus = model.NewsStatus,
                TagIds = model.TagIds,
                ActorAccountId = GetRequiredAccountId()
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(NewsArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMessage("Invalid news article data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _newsArticleService.UpdateAsync(new SaveNewsArticleRequestDto
            {
                NewsArticleId = model.NewsArticleId,
                NewsTitle = model.NewsTitle,
                Headline = model.Headline,
                NewsContent = model.NewsContent,
                NewsSource = model.NewsSource,
                CategoryId = model.CategoryId,
                NewsStatus = model.NewsStatus,
                TagIds = model.TagIds,
                ActorAccountId = GetRequiredAccountId()
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string newsArticleId)
        {
            var result = await _newsArticleService.DeleteAsync(newsArticleId);
            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        private short GetRequiredAccountId()
        {
            return (short)(HttpContext.Session.GetInt32(SessionKeys.AccountId) ?? 0);
        }

        private async Task SetLookupDataAsync()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
        }

        private void SetMessage(string message, bool isSuccess)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = isSuccess ? "success" : "danger";
        }

        private static NewsArticleFormViewModel MapNews(NewsArticleDto item)
        {
            return new NewsArticleFormViewModel
            {
                NewsArticleId = item.NewsArticleId,
                NewsTitle = item.NewsTitle,
                Headline = item.Headline,
                NewsContent = item.NewsContent,
                NewsSource = item.NewsSource,
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                NewsStatus = item.NewsStatus,
                CreatedByName = item.CreatedByName,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
                TagIds = item.TagIds.ToList(),
                TagNames = item.TagNames.ToList()
            };
        }
    }
}
