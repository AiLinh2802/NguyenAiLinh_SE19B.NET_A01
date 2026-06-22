namespace FUNewsManagement.BLL.Dtos
{
    public class ReportItemDto
    {
        public string NewsArticleId { get; set; } = string.Empty;
        public string NewsTitle { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public bool NewsStatus { get; set; }
    }
}
