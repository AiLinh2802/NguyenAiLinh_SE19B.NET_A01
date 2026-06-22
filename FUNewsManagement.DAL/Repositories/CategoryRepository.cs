using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDao _dao;

        public CategoryRepository(CategoryDao dao)
        {
            _dao = dao;
        }

        public Task<IReadOnlyList<Category>> GetAllAsync(string? search = null) =>
            _dao.GetAllAsync(search);

        public Task<Category?> GetByIdAsync(short categoryId) =>
            _dao.GetByIdAsync(categoryId);

        public Task<bool> HasNewsArticlesAsync(short categoryId) =>
            _dao.HasNewsArticlesAsync(categoryId);

        public Task AddAsync(Category category) =>
            _dao.AddAsync(category);

        public Task UpdateAsync(Category category) =>
            _dao.UpdateAsync(category);

        public Task DeleteAsync(Category category) =>
            _dao.DeleteAsync(category);
    }
}
