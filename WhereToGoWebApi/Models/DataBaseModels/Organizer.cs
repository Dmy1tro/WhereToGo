using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhereToGoWebApi.Models
{
    public class Organizer
    {
        [Key]
        public string OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual User User { get; set; }

        [Required, MaxLength(200)]
        public string InstType { get; set; }

        [Required, MaxLength(500)]
        public string PlaceName { get; set; }

        [Required, MaxLength(200)]
        public string Position { get; set; }

        [Required, MaxLength(50)]
        public string TelNumber { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
