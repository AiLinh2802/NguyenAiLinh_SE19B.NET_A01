using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.BLL.Interfaces;
using FUNewsManagement.DAL.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsRepository;
        private readonly ITagRepository _tagRepository;

        public NewsArticleService(INewsArticleRepository newsRepository, ITagRepository tagRepository)
        {
            _newsRepository = newsRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IReadOnlyList<NewsArticleDto>> GetAllAsync(string? search = null)
        {
            var items = await _newsRepository.GetAllAsync(search);
            return items.Select(n => n.ToDto()).ToList();
        }

        public async Task<IReadOnlyList<NewsArticleDto>> GetPublicNewsAsync(string? search = null)
        {
            var items = await _newsRepository.GetPublicNewsAsync(search);
            return items.Select(n => n.ToDto()).ToList();
        }

        public async Task<IReadOnlyList<NewsArticleDto>> GetByCreatorIdAsync(short creatorId, string? search = null)
        {
            var items = await _newsRepository.GetByCreatorIdAsync(creatorId, search);
            return items.Select(n => n.ToDto()).ToList();
        }

        public async Task<NewsArticleDto?> GetByIdAsync(string newsArticleId)
        {
            var article = await _newsRepository.GetByIdAsync(newsArticleId);
            return article?.ToDto();
        }

        public async Task<string> GetNextIdAsync()
        {
            return await _newsRepository.GetNextIdAsync();
        }

        public async Task<OperationResultDto> CreateAsync(SaveNewsArticleRequestDto request)
        {
            var newsArticleId = string.IsNullOrWhiteSpace(request.NewsArticleId)
                ? await _newsRepository.GetNextIdAsync()
                : request.NewsArticleId.Trim();

            if (await _newsRepository.ExistsAsync(newsArticleId))
            {
                return OperationResultDto.Failure("News article ID already exists.");
            }

            var article = new NewsArticle
            {
                NewsArticleId = newsArticleId,
                NewsTitle = request.NewsTitle.Trim(),
                Headline = request.Headline.Trim(),
                NewsContent = request.NewsContent.Trim(),
                NewsSource = request.NewsSource.Trim(),
                CategoryId = request.CategoryId,
                NewsStatus = request.NewsStatus,
                CreatedById = request.ActorAccountId,
                UpdatedById = request.ActorAccountId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            article.Tags = (await _tagRepository.GetByIdsAsync(request.TagIds)).ToList();

            await _newsRepository.AddAsync(article);
            return OperationResultDto.Success("News article created successfully.");
        }

        public async Task<OperationResultDto> UpdateAsync(SaveNewsArticleRequestDto request)
        {
            var article = await _newsRepository.GetByIdAsync(request.NewsArticleId);
            if (article == null)
            {
                return OperationResultDto.Failure("News article not found.");
            }

            article.NewsTitle = request.NewsTitle.Trim();
            article.Headline = request.Headline.Trim();
            article.NewsContent = request.NewsContent.Trim();
            article.NewsSource = request.NewsSource.Trim();
            article.CategoryId = request.CategoryId;
            article.NewsStatus = request.NewsStatus;
            article.UpdatedById = request.ActorAccountId;
            article.ModifiedDate = DateTime.Now;

            article.Tags.Clear();
            var tags = await _tagRepository.GetByIdsAsync(request.TagIds);
            foreach (var tag in tags)
            {
                article.Tags.Add(tag);
            }

            await _newsRepository.UpdateAsync(article);
            return OperationResultDto.Success("News article updated successfully.");
        }

        public async Task<OperationResultDto> DeleteAsync(string newsArticleId)
        {
            var article = await _newsRepository.GetByIdAsync(newsArticleId);
            if (article == null)
            {
                return OperationResultDto.Failure("News article not found.");
            }

            await _newsRepository.DeleteAsync(article);
            return OperationResultDto.Success("News article deleted successfully.");
        }
    }
}
