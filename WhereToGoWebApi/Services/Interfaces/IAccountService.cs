using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public interface IAccountService
    {
        Task<BaseResult> EditProfile(EditProfileViewModel model, string currentUserName);

        Task<BaseResult> ChangePassword(ChangePasswordViewModel model, string currentUserName);
    }
}
