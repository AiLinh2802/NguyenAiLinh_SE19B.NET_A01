using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;

namespace FUNewsManagement.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminCredentialProvider _adminCredentialProvider;
        private readonly ISystemAccountRepository _systemAccountRepository;

        public AuthService(
            IAdminCredentialProvider adminCredentialProvider,
            ISystemAccountRepository systemAccountRepository)
        {
            _adminCredentialProvider = adminCredentialProvider;
            _systemAccountRepository = systemAccountRepository;
        }

        public async Task<LoginResultDto> LoginAsync(string email, string password)
        {
            if (_adminCredentialProvider.IsAdmin(email, password))
            {
                return new LoginResultDto
                {
                    IsSuccess = true,
                    Email = email,
                    Role = "Admin",
                    AccountName = "Administrator"
                };
            }

            var account = await _systemAccountRepository.GetByEmailAndPasswordAsync(email, password);

            if (account == null)
            {
                return new LoginResultDto
                {
                    IsSuccess = false
                };
            }

            var role = account.AccountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };

            return new LoginResultDto
            {
                IsSuccess = true,
                AccountId = account.AccountId,
                Email = account.AccountEmail ?? string.Empty,
                Role = role,
                AccountName = account.AccountName ?? string.Empty
            };
        }
    }
}