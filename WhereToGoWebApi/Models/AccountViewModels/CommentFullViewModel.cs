using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class CommentFullViewModel : CommentViewModel
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
