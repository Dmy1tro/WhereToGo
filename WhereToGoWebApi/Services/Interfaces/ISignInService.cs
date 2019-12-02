using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public interface ISignInService
    {
        Task<LoginResult> LoginUser(LoginViewModel model);

        Task<RegisterResult> RegisterUser(RegisterUserViewModel model);

        Task<RegisterResult> RegisterCompany(RegisterCompanyViewModel model);
    }
}
