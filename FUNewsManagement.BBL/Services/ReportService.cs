using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;

namespace FUNewsManagement.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly INewsArticleRepository _newsRepository;

        public ReportService(INewsArticleRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IReadOnlyList<ReportItemDto>> GetReportAsync(DateTime startDate, DateTime endDate)
        {
            var items = await _newsRepository.GetReportAsync(startDate, endDate);
            return items.Select(n => new ReportItemDto
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle ?? string.Empty,
                CategoryName = n.Category?.CategoryName ?? string.Empty,
                CreatedByName = n.CreatedBy?.AccountName ?? string.Empty,
                CreatedDate = n.CreatedDate,
                NewsStatus = n.NewsStatus ?? false
            }).ToList();
        }
    }
}
