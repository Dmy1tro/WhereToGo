using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
        private const string failedToRemoveInDb = "Failed to remove in DataBase";
        private const string failedToUpdateInDb = "Failed to update in DataBase";
        private readonly UserManager<User> userManager;
        private readonly IEventDbRepository repository;
        private readonly IMapper mapper;

        public AccountService(UserManager<User> userManager, IEventDbRepository repository, IMapper mapper)
        {
            this.userManager = userManager;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResult> AddComment(CommentViewModel vmComment, string userId)
        {
            if (!await repository.Events.AnyAsync(x => x.EventId == vmComment.EventId))
                return new ErrorResult($"Event with id '{vmComment.EventId}' not found");

            var comment = new Comment(userId, vmComment.EventId, vmComment.Text);

            var result = await repository.CreateAndSaveEntityAsync<Comment>(comment);

            return result
                ? new OkResult() as BaseResult
                : new ErrorResult(failedToSaveInDb);
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

        public async Task<BaseResult> InviteUserToEvent(Invitation invitation, string userId)
        {
            var validation = await ValidateInvitation(invitation);

            if (!validation.IsValid)
                return validation;

            var user = await userManager.FindByIdAsync(userId);
            var userTo = await userManager.FindByNameAsync(invitation.UserName);

            EmailProcessor.SendInvitation(userTo.Email, user.UserName, $"http://localhost:5101/api/event/{invitation.EventId}");

            return new OkResult();
        }

        public async Task<BaseResult> RateEvent(RatingViewModel ratingView, string userId)
        {
            if (!await repository.Events.AnyAsync(x => x.EventId == ratingView.EventId))
                return new ErrorResult($"Event with id '{ratingView.EventId}' not found");

            var rating = new Rating(ratingView.Rate, ratingView.EventId, userId);

            var result = await repository.CreateAndSaveEntityAsync<Rating>(rating);

            return result
                ? new OkResult() as BaseResult
                : new ErrorResult(failedToSaveInDb);
        }

        public async Task<BaseResult> RemoveComment(int commentId, string userId)
        {
            var comment = await repository.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            if (comment is null)
                return new ErrorResult($"Comment with id '{commentId}' not found");

            if (comment.UserId != userId)
                return new ErrorResult("Comment do not belong to user");

            var result = await repository.RemoveAndSaveEntityAsync<Comment>(comment);

            return result
                ? new OkResult() as BaseResult
                : new ErrorResult(failedToRemoveInDb);
        }

        public async Task<BaseResult> SubscribeOnEvent(int eventId, string userId)
        {
            if (!repository.Events.Any(x => x.EventId == eventId))
                return new ErrorResult("Event not found");

            if (repository.UserEvents.Any(x => x.UserId.Equals(userId) && x.EventId == eventId))
                return new ErrorResult("You have subscribed already on this event");

            var userEvent = new UserEvent 
            {
                EventId = eventId,
                UserId = userId
            };

            var dbResult = await repository.CreateAndSaveEntityAsync(userEvent);

            if (!dbResult)
                return new ErrorResult(failedToSaveInDb);

            return new OkResult();
        }

        public async Task<BaseResult> UnScribeFromEvent(int eventId, string userId)
        {
            if (!repository.Events.Any(x => x.EventId == eventId))
                return new ErrorResult("Event not found");

            var userEvent = await repository.UserEvents.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);

            if (userEvent is null)
                return new ErrorResult("You have unscribed already on this event");

            var dbResult = await repository.RemoveAndSaveEntityAsync(userEvent);

            if (!dbResult)
                return new ErrorResult(failedToRemoveInDb);

            return new OkResult();
        }

        public async Task<BaseResult> UpdateComment(CommentViewModel vmComment, string userId)
        {
            var comment = await repository.Comments.FirstOrDefaultAsync(x => x.CommentId == vmComment.CommentId);

            if (comment is null)
                return new ErrorResult($"Comment with id '{vmComment.CommentId}' not found");

            if (comment.UserId != userId)
                return new ErrorResult("Comment do not belong to user");

            comment.BodyText = vmComment.Text;

            var result = await repository.UpdateAndSaveEntityAsync<Comment>(comment);

            return result
                ? new OkResult() as BaseResult
                : new ErrorResult(failedToUpdateInDb);
        }

        private async Task<BaseResult> ValidateInvitation(Invitation invitation)
        {
            var errors = string.Empty;

            if (!await repository.Events.AnyAsync(x => x.EventId == invitation.EventId))
                errors += $"Event with id '{invitation.EventId}' not found";

            if (!await repository.Users.AnyAsync(x => x.UserName == invitation.UserName))
                errors += $"User with name '{invitation.UserName}' not found";

            return string.IsNullOrEmpty(errors)
                ? new OkResult() as BaseResult
                : new ErrorResult(errors);
        }
    }
}
