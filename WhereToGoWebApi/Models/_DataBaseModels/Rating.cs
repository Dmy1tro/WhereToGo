using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models
{
    public class Rating
    {
        public Rating()
        {
        }

        public Rating(int rate, int eventId, string userId)
        {
            Rate = rate;
            EventId = eventId;
            UserId = userId;
        }

        [Key]
        public int RatingId { get; set; }

        [Required, Range(1, 10)]
        public int Rate { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required, ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int EventId { get; set; }
        [Required, ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
