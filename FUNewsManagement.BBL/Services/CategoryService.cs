using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(string? search = null)
        {
            var categories = await _categoryRepository.GetAllAsync(search);
            return categories.Select(c => c.ToDto()).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(short categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category?.ToDto();
        }

        public async Task<OperationResultDto> CreateAsync(SaveCategoryRequestDto request)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName.Trim(),
                CategoryDesciption = request.CategoryDescription.Trim(),
                ParentCategoryId = request.ParentCategoryId,
                IsActive = request.IsActive
            };

            await _categoryRepository.AddAsync(category);
            return OperationResultDto.Success("Category created successfully.");
        }

        public async Task<OperationResultDto> UpdateAsync(SaveCategoryRequestDto request)
        {
            if (!request.CategoryId.HasValue)
            {
                return OperationResultDto.Failure("Invalid category.");
            }

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category == null)
            {
                return OperationResultDto.Failure("Category not found.");
            }

            category.CategoryName = request.CategoryName.Trim();
            category.CategoryDesciption = request.CategoryDescription.Trim();
            category.ParentCategoryId = request.ParentCategoryId;
            category.IsActive = request.IsActive;

            await _categoryRepository.UpdateAsync(category);
            return OperationResultDto.Success("Category updated successfully.");
        }

        public async Task<OperationResultDto> DeleteAsync(short categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return OperationResultDto.Failure("Category not found.");
            }

            if (await _categoryRepository.HasNewsArticlesAsync(categoryId))
            {
                return OperationResultDto.Failure("Cannot delete category because it belongs to existing news articles.");
            }

            await _categoryRepository.DeleteAsync(category);
            return OperationResultDto.Success("Category deleted successfully.");
        }
    }
}
