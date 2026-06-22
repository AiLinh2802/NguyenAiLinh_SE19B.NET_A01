using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface ITagService
    {
        Task<IReadOnlyList<TagDto>> GetAllAsync(string? search = null);
        Task<TagDto?> GetByIdAsync(int tagId);
        Task<OperationResultDto> CreateAsync(SaveTagRequestDto request);
        Task<OperationResultDto> UpdateAsync(SaveTagRequestDto request);
        Task<OperationResultDto> DeleteAsync(int tagId);
    }
}
