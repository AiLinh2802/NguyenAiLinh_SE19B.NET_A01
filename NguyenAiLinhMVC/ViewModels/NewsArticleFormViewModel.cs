using System.ComponentModel.DataAnnotations;

namespace NguyenAiLinhMVC.ViewModels
{
    public class NewsArticleFormViewModel
    {
        [Required]
        [StringLength(20)]
        public string NewsArticleId { get; set; } = string.Empty;

        [Required]
        [StringLength(400)]
        public string NewsTitle { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Headline { get; set; } = string.Empty;

        [Required]
        [StringLength(4000)]
        public string NewsContent { get; set; } = string.Empty;

        [Required]
        [StringLength(400)]
        public string NewsSource { get; set; } = string.Empty;

        [Required]
        public short? CategoryId { get; set; }

        public bool NewsStatus { get; set; }

        public List<int> TagIds { get; set; } = new();

        public string CategoryName { get; set; } = string.Empty;
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<string> TagNames { get; set; } = new();
    }
}
