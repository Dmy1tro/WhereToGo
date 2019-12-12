using System.ComponentModel.DataAnnotations;

namespace WhereToGoWebApi.Models
{
    public class RegisterOrganaizerViewModel
    {
        [Required]
        public string InstType { get; set; }

        [Required]
        public string PlaceName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required, MaxLength(50)]
        public string TelNumber { get; set; }

        [Required]
        public object Doc { get; set; }
    }
}
