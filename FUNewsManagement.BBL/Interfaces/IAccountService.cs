using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IReadOnlyList<SystemAccountDto>> GetAllAsync(string? search = null);
        Task<SystemAccountDto?> GetByIdAsync(short accountId);
        Task<OperationResultDto> CreateAsync(SaveSystemAccountRequestDto request);
        Task<OperationResultDto> UpdateAsync(SaveSystemAccountRequestDto request);
        Task<OperationResultDto> DeleteAsync(short accountId);
    }
}
