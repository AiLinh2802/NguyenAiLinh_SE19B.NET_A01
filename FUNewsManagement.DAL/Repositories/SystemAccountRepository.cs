using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly SystemAccountDao _dao;

        public SystemAccountRepository(SystemAccountDao dao)
        {
            _dao = dao;
        }

        public Task<SystemAccount?> GetByEmailAndPasswordAsync(string email, string password) =>
            _dao.GetByEmailAndPasswordAsync(email, password);

        public Task<SystemAccount?> GetByIdAsync(short accountId) =>
            _dao.GetByIdAsync(accountId);

        public Task<IReadOnlyList<SystemAccount>> GetAllAsync(string? search = null) =>
            _dao.GetAllAsync(search);

        public Task<bool> ExistsByEmailAsync(string email, short? excludeAccountId = null) =>
            _dao.ExistsByEmailAsync(email, excludeAccountId);

        public Task<short> GetNextIdAsync() =>
            _dao.GetNextIdAsync();

        public Task AddAsync(SystemAccount account) =>
            _dao.AddAsync(account);

        public Task UpdateAsync(SystemAccount account) =>
            _dao.UpdateAsync(account);

        public Task DeleteAsync(SystemAccount account) =>
            _dao.DeleteAsync(account);
    }
}
