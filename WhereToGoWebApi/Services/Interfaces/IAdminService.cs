using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models.AccountViewModels;

namespace WhereToGoWebApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserProfileViewModel>> GetListOfUsers();

    }
}
