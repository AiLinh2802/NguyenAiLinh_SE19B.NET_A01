namespace FUNewsManagement.BLL.Dtos
{
    public class SaveTagRequestDto
    {
        public int? TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
