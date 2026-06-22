using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync(string? search = null);
        Task<Category?> GetByIdAsync(short categoryId);
        Task<bool> HasNewsArticlesAsync(short categoryId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
