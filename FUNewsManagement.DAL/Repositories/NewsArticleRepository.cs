using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly NewsArticleDao _dao;

        public NewsArticleRepository(NewsArticleDao dao)
        {
            _dao = dao;
        }

        public Task<IReadOnlyList<NewsArticle>> GetAllAsync(string? search = null) =>
            _dao.GetAllAsync(search);

        public Task<IReadOnlyList<NewsArticle>> GetPublicNewsAsync(string? search = null) =>
            _dao.GetPublicNewsAsync(search);

        public Task<IReadOnlyList<NewsArticle>> GetByCreatorIdAsync(short creatorId, string? search = null) =>
            _dao.GetByCreatorIdAsync(creatorId, search);

        public Task<IReadOnlyList<NewsArticle>> GetReportAsync(DateTime startDate, DateTime endDate) =>
            _dao.GetReportAsync(startDate, endDate);

        public Task<NewsArticle?> GetByIdAsync(string newsArticleId) =>
            _dao.GetByIdAsync(newsArticleId);

        public Task<string> GetNextIdAsync() =>
            _dao.GetNextIdAsync();

        public Task<bool> ExistsAsync(string newsArticleId) =>
            _dao.ExistsAsync(newsArticleId);

        public Task AddAsync(NewsArticle newsArticle) =>
            _dao.AddAsync(newsArticle);

        public Task UpdateAsync(NewsArticle newsArticle) =>
            _dao.UpdateAsync(newsArticle);

        public Task DeleteAsync(NewsArticle newsArticle) =>
            _dao.DeleteAsync(newsArticle);
    }
}
