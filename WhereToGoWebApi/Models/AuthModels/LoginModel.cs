using System.ComponentModel.DataAnnotations;

namespace WhereToGoWebApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
