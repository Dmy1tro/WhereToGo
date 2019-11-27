using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.AuthResults
{
    public class LoginResult
    {
        public LoginResult(string errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public LoginResult(string name, IEnumerable<string> roles, string token)
        {
            Name = name;
            Roles = roles;
            Token = token;
            IsValid = true;
        }

        public bool IsValid { get; }
        public string Errors { get; }
        public string Name { get; }
        public IEnumerable<string> Roles { get; }
        public string Token { get; }
    }
}
