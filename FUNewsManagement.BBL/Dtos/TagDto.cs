namespace FUNewsManagement.BLL.Dtos
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int NewsCount { get; set; }
    }
}
