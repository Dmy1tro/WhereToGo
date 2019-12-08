using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhereToGoWebApi.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required, MaxLength(300)]
        public string BodyText { get; set; }

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
