using System.ComponentModel.DataAnnotations;

namespace WhereToGoWebApi.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation pass do not match")]
        public string ConfirmPassword { get; set; }
    }
}
