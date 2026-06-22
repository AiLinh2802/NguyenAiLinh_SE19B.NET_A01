using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Interfaces
{
    public interface INewsArticleRepository
    {
        Task<IReadOnlyList<NewsArticle>> GetAllAsync(string? search = null);
        Task<IReadOnlyList<NewsArticle>> GetPublicNewsAsync(string? search = null);
        Task<IReadOnlyList<NewsArticle>> GetByCreatorIdAsync(short creatorId, string? search = null);
        Task<IReadOnlyList<NewsArticle>> GetReportAsync(DateTime startDate, DateTime endDate);
        Task<NewsArticle?> GetByIdAsync(string newsArticleId);
        Task<string> GetNextIdAsync();
        Task<bool> ExistsAsync(string newsArticleId);
        Task AddAsync(NewsArticle newsArticle);
        Task UpdateAsync(NewsArticle newsArticle);
        Task DeleteAsync(NewsArticle newsArticle);
    }
}
