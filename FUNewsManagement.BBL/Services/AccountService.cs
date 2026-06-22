using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISystemAccountRepository _accountRepository;

        public AccountService(ISystemAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IReadOnlyList<SystemAccountDto>> GetAllAsync(string? search = null)
        {
            var accounts = await _accountRepository.GetAllAsync(search);
            return accounts.Select(a => a.ToDto()).ToList();
        }

        public async Task<SystemAccountDto?> GetByIdAsync(short accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return account?.ToDto();
        }

        public async Task<OperationResultDto> CreateAsync(SaveSystemAccountRequestDto request)
        {
            if (await _accountRepository.ExistsByEmailAsync(request.AccountEmail))
            {
                return OperationResultDto.Failure("Email already exists.");
            }

            var nextId = await _accountRepository.GetNextIdAsync();
            var account = new SystemAccount
            {
                AccountId = nextId,
                AccountName = request.AccountName.Trim(),
                AccountEmail = request.AccountEmail.Trim(),
                AccountRole = request.AccountRole,
                AccountPassword = request.AccountPassword
            };

            await _accountRepository.AddAsync(account);
            return OperationResultDto.Success("Account created successfully.");
        }

        public async Task<OperationResultDto> UpdateAsync(SaveSystemAccountRequestDto request)
        {
            if (!request.AccountId.HasValue)
            {
                return OperationResultDto.Failure("Invalid account.");
            }

            var account = await _accountRepository.GetByIdAsync(request.AccountId.Value);
            if (account == null)
            {
                return OperationResultDto.Failure("Account not found.");
            }

            if (await _accountRepository.ExistsByEmailAsync(request.AccountEmail, request.AccountId.Value))
            {
                return OperationResultDto.Failure("Email already exists.");
            }

            account.AccountName = request.AccountName.Trim();
            account.AccountEmail = request.AccountEmail.Trim();
            account.AccountRole = request.AccountRole;
            account.AccountPassword = request.AccountPassword;

            await _accountRepository.UpdateAsync(account);
            return OperationResultDto.Success("Account updated successfully.");
        }

        public async Task<OperationResultDto> DeleteAsync(short accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return OperationResultDto.Failure("Account not found.");
            }

            if (account.NewsArticles.Any())
            {
                return OperationResultDto.Failure("Cannot delete an account that has created news articles.");
            }

            await _accountRepository.DeleteAsync(account);
            return OperationResultDto.Success("Account deleted successfully.");
        }
    }
}
