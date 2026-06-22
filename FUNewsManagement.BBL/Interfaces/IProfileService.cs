using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface IProfileService
    {
        Task<SystemAccountDto?> GetByIdAsync(short accountId);
        Task<OperationResultDto> UpdateProfileAsync(SaveSystemAccountRequestDto request);
    }
}
