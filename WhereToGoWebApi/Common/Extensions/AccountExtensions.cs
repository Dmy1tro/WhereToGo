using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Common.Extensions
{
    public static class AccountExtensions
    {
        public static string GetUserName(this IEnumerable<Claim> claims, string claimUserName) =>
            claims.FirstOrDefault(x => x.Type.Equals(claimUserName, StringComparison.InvariantCultureIgnoreCase))?
            .Value;
    }
}
