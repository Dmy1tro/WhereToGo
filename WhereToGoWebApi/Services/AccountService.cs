using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;

        public AccountService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<BaseResult> ChangePassword(ChangePasswordViewModel model, string currentUserName)
        {
            var user = await userManager.FindByNameAsync(currentUserName);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
                return new BaseResult(result.Errors.Select(x => x.Description));

            return new BaseResult();
        }

        public async Task<BaseResult> EditProfile(EditProfileViewModel model, string currentUserName)
        {
            var user = await userManager.FindByNameAsync(currentUserName);

            user.UserName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new BaseResult(result.Errors.Select(x => x.Description));

            return new BaseResult();
        }
    }
}
