using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface IReportService
    {
        Task<IReadOnlyList<ReportItemDto>> GetReportAsync(DateTime startDate, DateTime endDate);
    }
}
