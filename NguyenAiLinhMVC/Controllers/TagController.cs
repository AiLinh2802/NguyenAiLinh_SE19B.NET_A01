using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [SessionAuthorize("Staff")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var items = await _tagService.GetAllAsync(search);
            ViewBag.Search = search;
            return View(items.Select(MapTag).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMessage("Invalid tag data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _tagService.CreateAsync(new SaveTagRequestDto
            {
                TagName = model.TagName,
                Note = model.Note
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagFormViewModel model)
        {
            if (!ModelState.IsValid || !model.TagId.HasValue)
            {
                SetMessage("Invalid tag data.", false);
                return RedirectToAction(nameof(Index));
            }

            var result = await _tagService.UpdateAsync(new SaveTagRequestDto
            {
                TagId = model.TagId,
                TagName = model.TagName,
                Note = model.Note
            });

            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int tagId)
        {
            var result = await _tagService.DeleteAsync(tagId);
            SetMessage(result.Message, result.IsSuccess);
            return RedirectToAction(nameof(Index));
        }

        private void SetMessage(string message, bool isSuccess)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = isSuccess ? "success" : "danger";
        }

        private static TagFormViewModel MapTag(TagDto item)
        {
            return new TagFormViewModel
            {
                TagId = item.TagId,
                TagName = item.TagName,
                Note = item.Note,
                NewsCount = item.NewsCount
            };
        }
    }
}
