using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;

namespace FUNewsManagement.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ISystemAccountRepository _accountRepository;

        public ProfileService(ISystemAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<SystemAccountDto?> GetByIdAsync(short accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return account?.ToDto();
        }

        public async Task<OperationResultDto> UpdateProfileAsync(SaveSystemAccountRequestDto request)
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
            account.AccountPassword = request.AccountPassword;

            await _accountRepository.UpdateAsync(account);
            return OperationResultDto.Success("Profile updated successfully.");
        }
    }
}
