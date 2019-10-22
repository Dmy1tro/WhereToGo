using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhereToGoWebApi.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }

        [Required]
        public int TotalMembers { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required, ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int EventId { get; set; }
        [Required, ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public virtual ICollection<EventMeeting> EventMeetings { get; set; } = new List<EventMeeting>();

    }
}
