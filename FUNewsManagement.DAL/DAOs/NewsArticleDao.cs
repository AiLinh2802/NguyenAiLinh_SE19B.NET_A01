using FUNewsManagement.DAL.Data;
using FUNewsManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagement.DAL.DAOs
{
    public class NewsArticleDao
    {
        private readonly FUNewsManagementContext _context;

        public NewsArticleDao(FUNewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<NewsArticle>> GetAllAsync(string? search = null)
        {
            var query = BuildQuery();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(n =>
                    n.NewsArticleId.Contains(search) ||
                    (n.NewsTitle ?? string.Empty).Contains(search) ||
                    n.Headline.Contains(search) ||
                    (n.NewsContent ?? string.Empty).Contains(search) ||
                    (n.NewsSource ?? string.Empty).Contains(search) ||
                    (n.Category != null ? n.Category.CategoryName : string.Empty).Contains(search) ||
                    (n.CreatedBy != null ? n.CreatedBy.AccountName ?? string.Empty : string.Empty).Contains(search) ||
                    n.Tags.Any(t => (t.TagName ?? string.Empty).Contains(search)));
            }

            return await query
                .OrderByDescending(n => n.CreatedDate)
                .ThenByDescending(n => n.ModifiedDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<NewsArticle>> GetPublicNewsAsync(string? search = null)
        {
            var query = BuildQuery().Where(n => n.NewsStatus == true);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(n =>
                    (n.NewsTitle ?? string.Empty).Contains(search) ||
                    n.Headline.Contains(search) ||
                    (n.NewsContent ?? string.Empty).Contains(search));
            }

            return await query
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<NewsArticle>> GetByCreatorIdAsync(short creatorId, string? search = null)
        {
            var query = BuildQuery().Where(n => n.CreatedById == creatorId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(n =>
                    n.NewsArticleId.Contains(search) ||
                    (n.NewsTitle ?? string.Empty).Contains(search) ||
                    n.Headline.Contains(search) ||
                    (n.Category != null ? n.Category.CategoryName : string.Empty).Contains(search) ||
                    n.Tags.Any(t => (t.TagName ?? string.Empty).Contains(search)));
            }

            return await query
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<NewsArticle>> GetReportAsync(DateTime startDate, DateTime endDate)
        {
            var inclusiveEndDate = endDate.Date.AddDays(1).AddTicks(-1);

            return await BuildQuery()
                .Where(n => n.CreatedDate.HasValue &&
                            n.CreatedDate.Value >= startDate.Date &&
                            n.CreatedDate.Value <= inclusiveEndDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<NewsArticle?> GetByIdAsync(string newsArticleId)
        {
            return await BuildQuery()
                .FirstOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
        }

        public async Task<string> GetNextIdAsync()
        {
            var ids = await _context.NewsArticles
                .Select(n => n.NewsArticleId)
                .ToListAsync();

            var maxNumericId = ids
                .Select(id => int.TryParse(id, out var value) ? value : 0)
                .DefaultIfEmpty(0)
                .Max();

            return (maxNumericId + 1).ToString();
        }

        public async Task<bool> ExistsAsync(string newsArticleId)
        {
            return await _context.NewsArticles.AnyAsync(n => n.NewsArticleId == newsArticleId);
        }

        public async Task AddAsync(NewsArticle newsArticle)
        {
            _context.NewsArticles.Add(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NewsArticle newsArticle)
        {
            _context.NewsArticles.Update(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NewsArticle newsArticle)
        {
            newsArticle.Tags.Clear();
            _context.NewsArticles.Remove(newsArticle);
            await _context.SaveChangesAsync();
        }

        private IQueryable<NewsArticle> BuildQuery()
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .AsQueryable();
        }
    }
}
