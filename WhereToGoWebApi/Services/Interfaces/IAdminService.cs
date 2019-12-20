using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserProfileViewModel>> GetListOfUsers();
        Task<UserProfileViewModel> GetUserProfile(string userId);
        Task<BaseResult> RemoveComment(int commentId);
    }
}
