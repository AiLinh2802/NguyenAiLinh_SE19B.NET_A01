using System.ComponentModel.DataAnnotations;

namespace NguyenAiLinhMVC.ViewModels
{
    public class TagFormViewModel
    {
        public int? TagId { get; set; }

        [Required]
        [StringLength(50)]
        public string TagName { get; set; } = string.Empty;

        [StringLength(400)]
        public string Note { get; set; } = string.Empty;

        public int NewsCount { get; set; }
    }
}
