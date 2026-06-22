using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.DAL.DAOs;

namespace NguyenAiLinhMVC.ViewModels
{
    public class DashboardViewModel
    {
        public string Role { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public int TotalAccounts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalNewsArticles { get; set; }
        public int TotalActiveNewsArticles { get; set; }
        public int MyTotalNewsArticles { get; set; }
        public int MyActiveNewsArticles { get; set; }
        public int MyInactiveNewsArticles { get; set; }
        public IReadOnlyList<NewsArticleDto> LatestNewsArticles { get; set; } = Array.Empty<NewsArticleDto>();
    }
}
