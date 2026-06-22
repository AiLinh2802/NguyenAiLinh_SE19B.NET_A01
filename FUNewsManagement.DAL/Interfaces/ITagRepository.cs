using Azure;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Interfaces
{
    public interface ITagRepository
    {
        Task<IReadOnlyList<Tag>> GetAllAsync(string? search = null);
        Task<IReadOnlyList<Tag>> GetByIdsAsync(IEnumerable<int> tagIds);
        Task<Tag?> GetByIdAsync(int tagId);
        Task<int> GetNextIdAsync();
        Task<bool> IsAssignedAsync(int tagId);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag tag);
    }
}
