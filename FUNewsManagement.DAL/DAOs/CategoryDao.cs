using FUNewsManagement.DAL.Data;
using FUNewsManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagement.DAL.DAOs
{
    public class CategoryDao
    {
        private readonly FUNewsManagementContext _context;

        public CategoryDao(FUNewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(string? search = null)
        {
            var query = _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.NewsArticles)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(c =>
                    c.CategoryName.Contains(search) ||
                    c.CategoryDesciption.Contains(search));
            }

            return await query
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(short categoryId)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.NewsArticles)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<bool> HasNewsArticlesAsync(short categoryId)
        {
            return await _context.NewsArticles.AnyAsync(n => n.CategoryId == categoryId);
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
