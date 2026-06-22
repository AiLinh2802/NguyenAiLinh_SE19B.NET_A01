using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [Route("[controller]")]
    [Route("Categories")]
    [SessionAuthorize("Staff")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? search)
        {
            var items = await _categoryService.GetAllAsync(search);
            ViewBag.Search = search;
            ViewBag.ParentOptions = items;
            return View(items.Select(MapCategory).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMessage("Invalid category data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _categoryService.CreateAsync(new SaveCategoryRequestDto
            {
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription,
                ParentCategoryId = model.ParentCategoryId,
                IsActive = model.IsActive
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid || !model.CategoryId.HasValue)
            {
                SetMessage("Invalid category data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _categoryService.UpdateAsync(new SaveCategoryRequestDto
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription,
                ParentCategoryId = model.ParentCategoryId,
                IsActive = model.IsActive
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(short categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId);
            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        private void SetMessage(string message, bool isSuccess)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = isSuccess ? "success" : "danger";
        }

        private static CategoryFormViewModel MapCategory(CategoryDto item)
        {
            return new CategoryFormViewModel
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                CategoryDescription = item.CategoryDescription,
                ParentCategoryId = item.ParentCategoryId,
                ParentCategoryName = item.ParentCategoryName,
                IsActive = item.IsActive,
                NewsCount = item.NewsCount
            };
        }
    }
}
