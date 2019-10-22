using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models
{
    public class User : IdentityUser
    {
        public virtual Organizer Organizer { get; set; }

        public virtual ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public virtual ICollection<EventMeeting> EventMeetings { get; set; } = new List<EventMeeting>();
    }
}
