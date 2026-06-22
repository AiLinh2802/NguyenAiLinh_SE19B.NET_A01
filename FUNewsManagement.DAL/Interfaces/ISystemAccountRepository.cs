using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Interfaces
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount?> GetByEmailAndPasswordAsync(string email, string password);
        Task<SystemAccount?> GetByIdAsync(short accountId);
        Task<IReadOnlyList<SystemAccount>> GetAllAsync(string? search = null);
        Task<bool> ExistsByEmailAsync(string email, short? excludeAccountId = null);
        Task<short> GetNextIdAsync();
        Task AddAsync(SystemAccount account);
        Task UpdateAsync(SystemAccount account);
        Task DeleteAsync(SystemAccount account);
    }
}
