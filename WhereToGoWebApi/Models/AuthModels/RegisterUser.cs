using System.ComponentModel.DataAnnotations;

namespace WhereToGoWebApi.Models
{
    public class RegisterUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation pass do not match")]
        public string ConfirmPassword { get; set; }

        public bool AcceptRules { get; set; }
    }
}
