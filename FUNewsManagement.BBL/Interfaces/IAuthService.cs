using FUNewsManagement.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto> 
            LoginAsync(string email, string password);
    }
}