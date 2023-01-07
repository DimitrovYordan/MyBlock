using System.ComponentModel.DataAnnotations;

namespace MyBlock.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(32, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
