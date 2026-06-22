using Azure;
using FUNewsManagement.DAL.Data;
using FUNewsManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagement.DAL.DAOs
{
    public class TagDao
    {
        private readonly FUNewsManagementContext _context;

        public TagDao(FUNewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Tag>> GetAllAsync(string? search = null)
        {
            var query = _context.Tags
                .Include(t => t.NewsArticles)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(t =>
                    (t.TagName ?? string.Empty).Contains(search) ||
                    (t.Note ?? string.Empty).Contains(search));
            }

            return await query
                .OrderBy(t => t.TagName)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Tag>> GetByIdsAsync(IEnumerable<int> tagIds)
        {
            var ids = tagIds.Distinct().ToList();
            return await _context.Tags
                .Where(t => ids.Contains(t.TagId))
                .OrderBy(t => t.TagName)
                .ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int tagId)
        {
            return await _context.Tags
                .Include(t => t.NewsArticles)
                .FirstOrDefaultAsync(t => t.TagId == tagId);
        }

        public async Task<int> GetNextIdAsync()
        {
            var maxId = await _context.Tags
                .Select(t => (int?)t.TagId)
                .MaxAsync();

            return (maxId ?? 0) + 1;
        }

        public async Task<bool> IsAssignedAsync(int tagId)
        {
            return await _context.Tags
                .AnyAsync(t => t.TagId == tagId && t.NewsArticles.Any());
        }

        public async Task AddAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}
