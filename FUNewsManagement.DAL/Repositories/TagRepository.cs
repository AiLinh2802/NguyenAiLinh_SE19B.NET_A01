using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagDao _dao;

        public TagRepository(TagDao dao)
        {
            _dao = dao;
        }

        public Task<IReadOnlyList<Tag>> GetAllAsync(string? search = null) =>
            _dao.GetAllAsync(search);

        public Task<IReadOnlyList<Tag>> GetByIdsAsync(IEnumerable<int> tagIds) =>
            _dao.GetByIdsAsync(tagIds);

        public Task<Tag?> GetByIdAsync(int tagId) =>
            _dao.GetByIdAsync(tagId);

        public Task<int> GetNextIdAsync() =>
            _dao.GetNextIdAsync();

        public Task<bool> IsAssignedAsync(int tagId) =>
            _dao.IsAssignedAsync(tagId);

        public Task AddAsync(Tag tag) =>
            _dao.AddAsync(tag);

        public Task UpdateAsync(Tag tag) =>
            _dao.UpdateAsync(tag);

        public Task DeleteAsync(Tag tag) =>
            _dao.DeleteAsync(tag);
    }
}
