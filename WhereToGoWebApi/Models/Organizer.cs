using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models
{
    public class Organizer
    {
        [Key]
        public string OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual User User { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
