using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllAsync(string? search = null);
        Task<CategoryDto?> GetByIdAsync(short categoryId);
        Task<OperationResultDto> CreateAsync(SaveCategoryRequestDto request);
        Task<OperationResultDto> UpdateAsync(SaveCategoryRequestDto request);
        Task<OperationResultDto> DeleteAsync(short categoryId);
    }
}
