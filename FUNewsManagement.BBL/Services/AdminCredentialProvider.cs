using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.BLL.Options;
using Microsoft.Extensions.Options;

namespace FUNewsManagement.BLL.Services
{
    public class AdminCredentialProvider : IAdminCredentialProvider
    {
        private readonly AdminAccountOptions _adminAccount;

        public AdminCredentialProvider(IOptions<AdminAccountOptions> options)
        {
            _adminAccount = options.Value;
        }

        public bool IsAdmin(string email, string password)
        {
            return email.Equals(_adminAccount.Email, StringComparison.OrdinalIgnoreCase)
                   && password == _adminAccount.Password;
        }
    }
}