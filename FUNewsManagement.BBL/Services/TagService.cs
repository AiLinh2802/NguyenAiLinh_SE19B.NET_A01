using Azure;
using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IReadOnlyList<TagDto>> GetAllAsync(string? search = null)
        {
            var tags = await _tagRepository.GetAllAsync(search);
            return tags.Select(t => t.ToDto()).ToList();
        }

        public async Task<TagDto?> GetByIdAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            return tag?.ToDto();
        }

        public async Task<OperationResultDto> CreateAsync(SaveTagRequestDto request)
        {
            var nextId = await _tagRepository.GetNextIdAsync();
            var tag = new Tag
            {
                TagId = nextId,
                TagName = request.TagName.Trim(),
                Note = request.Note.Trim()
            };

            await _tagRepository.AddAsync(tag);
            return OperationResultDto.Success("Tag created successfully.");
        }

        public async Task<OperationResultDto> UpdateAsync(SaveTagRequestDto request)
        {
            if (!request.TagId.HasValue)
            {
                return OperationResultDto.Failure("Invalid tag.");
            }

            var tag = await _tagRepository.GetByIdAsync(request.TagId.Value);
            if (tag == null)
            {
                return OperationResultDto.Failure("Tag not found.");
            }

            tag.TagName = request.TagName.Trim();
            tag.Note = request.Note.Trim();

            await _tagRepository.UpdateAsync(tag);
            return OperationResultDto.Success("Tag updated successfully.");
        }

        public async Task<OperationResultDto> DeleteAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            if (tag == null)
            {
                return OperationResultDto.Failure("Tag not found.");
            }

            if (await _tagRepository.IsAssignedAsync(tagId))
            {
                return OperationResultDto.Failure("Cannot delete tag because it is assigned to a news article.");
            }

            await _tagRepository.DeleteAsync(tag);
            return OperationResultDto.Success("Tag deleted successfully.");
        }
    }
}
