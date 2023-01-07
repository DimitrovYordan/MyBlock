using System.ComponentModel.DataAnnotations;

namespace MyBlock.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        public string ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(32, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(32, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        public string Email { get; set; }
    }
}
