namespace FUNewsManagement.BLL.Dtos
{
    public class SaveCategoryRequestDto
    {
        public short? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public short? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
