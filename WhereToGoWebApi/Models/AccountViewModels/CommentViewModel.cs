using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required, MaxLength(300)]
        public string Text { get; set; }
    }
}
