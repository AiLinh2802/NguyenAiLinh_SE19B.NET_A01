using System.ComponentModel.DataAnnotations;
using FUNewsManagement.BLL.Dtos;

namespace NguyenAiLinhMVC.ViewModels
{
    public class ReportFilterViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IReadOnlyList<ReportItemDto> Items { get; set; } = Array.Empty<ReportItemDto>();
    }
}
