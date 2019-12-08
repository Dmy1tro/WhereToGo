using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Common.Extensions
{
    public static class AccountExtensions
    {

        public static string GetUserClaim(this IEnumerable<Claim> claims, string claim) =>
            claims.FirstOrDefault(x => x.Type.Equals(claim, StringComparison.InvariantCultureIgnoreCase))?
            .Value;
    }
}
