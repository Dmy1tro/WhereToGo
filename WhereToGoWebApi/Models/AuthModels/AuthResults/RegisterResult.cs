using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.AuthResults
{
    public class RegisterResult
    {
        public RegisterResult(IEnumerable<string> errors)
        {
            Errors = string.Join("\n", errors);
            IsValid = false;
        }

        public RegisterResult(string errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public RegisterResult(User user)
        {
            User = user;
            IsValid = true;
        }

        public RegisterResult()
        {
            IsValid = true;
        }

        public string Errors { get; }
        public bool IsValid { get; }
        public User User { get; }
    }
}
