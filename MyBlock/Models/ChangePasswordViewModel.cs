using System.ComponentModel.DataAnnotations;

namespace MyBlock.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(25, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(5, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string NewPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(25, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(5, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string ConfirmPassword { get; set; }
    }
}
