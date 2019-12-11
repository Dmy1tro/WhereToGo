using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private const string failedToSaveInDb = "Failed to save in DataBase";
        private readonly UserManager<User> userManager;
        private readonly IEventDbRepository repository;
        private readonly IMapper mapper;

        public AccountService(UserManager<User> userManager, IEventDbRepository repository, IMapper mapper)
        {
            this.userManager = userManager;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResult> ChangePassword(ChangePasswordViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
                return new ErrorResult(result.Errors.Select(x => x.Description));

            return new OkResult();
        }

        public async Task<BaseResult> EditOrganaizerInfo(OrganizerProfileViewModel model, string userId)
        {
            var organizer = mapper.Map<Organizer>(model);
            organizer.OrganizerId = userId;

            var result = await repository.UpdateAndSaveEntityAsync(organizer);

            return result ?
                (BaseResult)new OkResult() :
                (BaseResult)new ErrorResult(failedToSaveInDb);
        }

        public async Task<BaseResult> EditProfile(UserProfileViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            user.UserName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new ErrorResult(result.Errors.Select(x => x.Description));

            return new OkResult();
        }

        public async Task<BaseResult> SubscribeOnEvent(int eventId, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (!repository.Events.Any(x => x.EventId == eventId))
                return new ErrorResult("Event not found");

            if (repository.UserEvents.Any(x => x.UserId.Equals(user.Id) && x.EventId == eventId))
                return new ErrorResult("You have subscribed already on this event");

            var userEvent = new UserEvent 
            {
                EventId = eventId,
                UserId = user.Id
            };

            var dbResult = await repository.CreateAndSaveEntityAsync(userEvent);

            if (!dbResult)
                return new ErrorResult(failedToSaveInDb);

            return new OkResult();
        }
    }
}
