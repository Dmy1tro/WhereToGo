using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.Interfaces;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AdminService(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserProfileViewModel>> GetListOfUsers() =>
            await userManager.Users
            .ProjectTo<UserProfileViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<UserProfileViewModel> GetUserProfile(string userId) =>
            await userManager.Users
            .Where(x => x.Id == userId)
            .ProjectTo<UserProfileViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public Task<BaseResult> RemoveComment(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
