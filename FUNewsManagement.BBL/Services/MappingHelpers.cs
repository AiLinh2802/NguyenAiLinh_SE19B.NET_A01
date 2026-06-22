using Azure;
using FUNewsManagement.BLL.Constants;
using FUNewsManagement.BLL.Dtos;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services
{
    internal static class MappingHelpers
    {
        public static string ToRoleName(int? accountRole)
        {
            return accountRole switch
            {
                1 => SystemRoles.Staff,
                2 => SystemRoles.Lecturer,
                _ => "Unknown"
            };
        }

        public static SystemAccountDto ToDto(this SystemAccount account)
        {
            return new SystemAccountDto
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName ?? string.Empty,
                AccountEmail = account.AccountEmail ?? string.Empty,
                AccountRole = account.AccountRole ?? 0,
                RoleName = ToRoleName(account.AccountRole),
                CreatedNewsCount = account.NewsArticles.Count
            };
        }

        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDesciption,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategory?.CategoryName,
                IsActive = category.IsActive ?? false,
                NewsCount = category.NewsArticles.Count
            };
        }

        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName ?? string.Empty,
                Note = tag.Note ?? string.Empty,
                NewsCount = tag.NewsArticles.Count
            };
        }

        public static NewsArticleDto ToDto(this NewsArticle article)
        {
            return new NewsArticleDto
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle ?? string.Empty,
                Headline = article.Headline,
                NewsContent = article.NewsContent ?? string.Empty,
                NewsSource = article.NewsSource ?? string.Empty,
                CategoryId = article.CategoryId,
                CategoryName = article.Category?.CategoryName ?? string.Empty,
                NewsStatus = article.NewsStatus ?? false,
                CreatedById = article.CreatedById,
                CreatedByName = article.CreatedBy?.AccountName ?? string.Empty,
                CreatedDate = article.CreatedDate,
                UpdatedById = article.UpdatedById,
                ModifiedDate = article.ModifiedDate,
                TagIds = article.Tags.Select(t => t.TagId).ToList(),
                TagNames = article.Tags.Select(t => t.TagName ?? string.Empty).ToList()
            };
        }
    }
}
