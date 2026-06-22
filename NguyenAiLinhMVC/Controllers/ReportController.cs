using FUNewsManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NguyenAiLinhMVC.Infrastructure;
using NguyenAiLinhMVC.ViewModels;

namespace NguyenAiLinhMVC.Controllers
{
    [Route("[controller]")]
    [Route("Reports")]
    [SessionAuthorize("Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var model = new ReportFilterViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            if (startDate.HasValue && endDate.HasValue && startDate <= endDate)
            {
                model.Items = await _reportService.GetReportAsync(startDate.Value, endDate.Value);
            }

            return View(model);
        }
    }
}
