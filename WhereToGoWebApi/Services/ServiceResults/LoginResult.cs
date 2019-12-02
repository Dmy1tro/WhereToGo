using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Services.ServiceResults
{
    public class LoginResult : BaseResult
    {
        public LoginResult(string errors): base(errors) {  }

        public LoginResult(string name, IEnumerable<string> roles, string token) : base()
        {
            Name = name;
            Roles = roles;
            Token = token;
        }

        public string Name { get; }
        public IEnumerable<string> Roles { get; }
        public string Token { get; }
    }
}
