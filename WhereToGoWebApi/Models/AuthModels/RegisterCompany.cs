using System.ComponentModel.DataAnnotations;

namespace WhereToGoWebApi.Models
{
    public class RegisterCompany
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string InstType { get; set; }

        [Required]
        public string PlaceName { get; set; }

        [Required]
        public string Position { get; set; }

        [Phone]
        public string TelNumber { get; set; }

        [Required]
        public object Doc { get; set; }
    }
}
