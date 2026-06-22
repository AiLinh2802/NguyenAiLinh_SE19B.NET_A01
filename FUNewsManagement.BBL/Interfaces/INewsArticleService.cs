using FUNewsManagement.BLL.Dtos;

namespace FUNewsManagement.BLL.Interfaces
{
    public interface INewsArticleService
    {
        Task<IReadOnlyList<NewsArticleDto>> GetAllAsync(string? search = null);
        Task<IReadOnlyList<NewsArticleDto>> GetPublicNewsAsync(string? search = null);
        Task<IReadOnlyList<NewsArticleDto>> GetByCreatorIdAsync(short creatorId, string? search = null);
        Task<NewsArticleDto?> GetByIdAsync(string newsArticleId);
        Task<string> GetNextIdAsync();
        Task<OperationResultDto> CreateAsync(SaveNewsArticleRequestDto request);
        Task<OperationResultDto> UpdateAsync(SaveNewsArticleRequestDto request);
        Task<OperationResultDto> DeleteAsync(string newsArticleId);
    }
}
