using System.ComponentModel.DataAnnotations;

namespace NguyenAiLinhMVC.ViewModels
{
    public class CategoryFormViewModel
    {
        public short? CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string CategoryDescription { get; set; } = string.Empty;

        public short? ParentCategoryId { get; set; }

        public bool IsActive { get; set; }

        public string? ParentCategoryName { get; set; }
        public int NewsCount { get; set; }
    }
}
