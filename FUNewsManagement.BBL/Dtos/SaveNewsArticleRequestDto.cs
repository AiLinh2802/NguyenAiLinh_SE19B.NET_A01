namespace FUNewsManagement.BLL.Dtos
{
    public class SaveNewsArticleRequestDto
    {
        public string NewsArticleId { get; set; } = string.Empty;
        public string NewsTitle { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string NewsContent { get; set; } = string.Empty;
        public string NewsSource { get; set; } = string.Empty;
        public short? CategoryId { get; set; }
        public bool NewsStatus { get; set; }
        public short ActorAccountId { get; set; }
        public IReadOnlyList<int> TagIds { get; set; } = Array.Empty<int>();
    }
}
