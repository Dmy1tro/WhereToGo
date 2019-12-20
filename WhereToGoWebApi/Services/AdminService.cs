using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.Interfaces;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly IEventDbRepository repository;
        private readonly IMapper mapper;

        public AdminService(UserManager<User> userManager, 
                            IEventDbRepository repository,
                            IMapper mapper)
        {
            this.userManager = userManager;
            this.repository = repository;
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

        public async Task<BaseResult> RemoveComment(int commentId)
        {
            var comment = await repository.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            if (comment is null)
                return new ErrorResult($"Comment with id '{commentId}' not found");

            var result = await repository.RemoveAndSaveEntityAsync<Comment>(comment);

            return result
                ? new OkResult() as BaseResult
                : new ErrorResult(Exceptions.failedToRemoveInDb);
        }
    }
}
