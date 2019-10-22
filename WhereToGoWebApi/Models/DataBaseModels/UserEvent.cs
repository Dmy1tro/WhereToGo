using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhereToGoWebApi.Models
{
    public class UserEvent
    {
        [Key]
        public int UserEventId { get; set; }

        public string UserId { get; set; }
        [Required, ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int EventId { get; set; }
        [Required, ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
