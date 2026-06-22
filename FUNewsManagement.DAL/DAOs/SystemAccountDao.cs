using FUNewsManagement.DAL.Data;
using FUNewsManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagement.DAL.DAOs
{
    public class SystemAccountDao
    {
        private readonly FUNewsManagementContext _context;

        public SystemAccountDao(FUNewsManagementContext context)
        {
            _context = context;
        }

        public async Task<SystemAccount?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.SystemAccounts
                .FirstOrDefaultAsync(a =>
                    a.AccountEmail == email &&
                    a.AccountPassword == password);
        }

        public async Task<SystemAccount?> GetByIdAsync(short accountId)
        {
            return await _context.SystemAccounts
                .Include(a => a.NewsArticles)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<IReadOnlyList<SystemAccount>> GetAllAsync(string? search = null)
        {
            var query = _context.SystemAccounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                var roleKeyword = search.Equals("staff", StringComparison.OrdinalIgnoreCase)
                    ? 1
                    : search.Equals("lecturer", StringComparison.OrdinalIgnoreCase)
                        ? 2
                        : (int?)null;
                query = query.Where(a =>
                    (a.AccountName ?? string.Empty).Contains(search) ||
                    (a.AccountEmail ?? string.Empty).Contains(search) ||
                    (roleKeyword.HasValue && a.AccountRole == roleKeyword.Value));
            }

            return await query
                .OrderBy(a => a.AccountRole)
                .ThenBy(a => a.AccountName)
                .ToListAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email, short? excludeAccountId = null)
        {
            return await _context.SystemAccounts.AnyAsync(a =>
                a.AccountEmail == email &&
                (!excludeAccountId.HasValue || a.AccountId != excludeAccountId.Value));
        }

        public async Task<short> GetNextIdAsync()
        {
            var maxId = await _context.SystemAccounts
                .Select(a => (short?)a.AccountId)
                .MaxAsync();

            return (short)((maxId ?? 0) + 1);
        }

        public async Task AddAsync(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SystemAccount account)
        {
            _context.SystemAccounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}
