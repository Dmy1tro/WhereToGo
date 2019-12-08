using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhereToGoWebApi.Models
{
    public class EventMeeting
    {
        [Key]
        public int EventMeetingId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required, ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int MeetingId { get; set; }
        [Required, ForeignKey("MeetingId")]
        public virtual Meeting Meeting { get; set; }
    }
}
