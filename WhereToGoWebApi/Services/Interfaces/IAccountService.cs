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
        Task<BaseResult> EditProfile(UserProfileViewModel model, string userId);

        Task<BaseResult> EditOrganaizerInfo(OrganizerProfileViewModel model, string userId);

        Task<BaseResult> ChangePassword(ChangePasswordViewModel model, string userId);

        Task<BaseResult> SubscribeOnEvent(int eventId, string userId);
    }
}
