using System.ComponentModel.DataAnnotations;

namespace NguyenAiLinhMVC.ViewModels
{
    public class ProfileFormViewModel
    {
        [Required]
        public short AccountId { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(70)]
        public string AccountEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(70, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
