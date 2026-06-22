using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagement.BLL.Dtos
{
    public class LoginResultDto
    {
        public bool IsSuccess { get; set; }
        public short? AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
    }
}
