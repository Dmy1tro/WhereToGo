using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly IEventDbRepository repository;

        public AccountService(UserManager<User> userManager, IEventDbRepository repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task<BaseResult> ChangePassword(ChangePasswordViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
                return new BaseResult(result.Errors.Select(x => x.Description));

            return new BaseResult();
        }

        public async Task<BaseResult> EditProfile(UserProfileViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            user.UserName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new BaseResult(result.Errors.Select(x => x.Description));

            return new BaseResult();
        }

        public async Task<BaseResult> SubscribeOnEvent(int eventId, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (!repository.Events.Any(x => x.EventId == eventId))
                return new BaseResult("Event not found");

            if (repository.UserEvents.Any(x => x.UserId.Equals(user.Id) && x.EventId == eventId))
                return new BaseResult("You have subscribed already on this event");

            var userEvent = new UserEvent 
            {
                EventId = eventId,
                UserId = user.Id
            };

            var dbResult = await repository.CreateAndSaveEntityAsync(userEvent);

            if (!dbResult)
                return new BaseResult("Failed to save in DataBase");

            return new BaseResult();
        }
    }
}
