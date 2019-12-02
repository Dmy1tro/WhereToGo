using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.Services.ServiceResults
{
    public class RegisterResult : BaseResult
    {
        public RegisterResult(IEnumerable<string> errors) : base(errors) { }

        public RegisterResult(string errors) : base(errors) { }

        public RegisterResult(User user) : base()
        {
            User = user;
        }

        public RegisterResult() : base() { }

        public User User { get; }
    }
}
