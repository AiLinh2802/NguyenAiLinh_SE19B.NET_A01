using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface IAdminCredentialProvider
    {
        bool IsAdmin(string email, string password);
    }
}