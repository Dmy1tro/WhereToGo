using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class RatingViewModel
    {
        [Required, Range(1, 10)]
        public int Rate { get; set; }

        [Required]
        public int EventId { get; set; }

    }
}
