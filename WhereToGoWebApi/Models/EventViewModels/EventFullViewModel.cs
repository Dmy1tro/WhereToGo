using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models.AccountViewModels;

namespace WhereToGoWebApi.Models.EventViewModels
{
    public class EventFullViewModel : EventViewModel
    {
        public List<CommentFullViewModel> Comments { get; set; } = new List<CommentFullViewModel>();
        public double AvgRate { get; set; }
    }
}
