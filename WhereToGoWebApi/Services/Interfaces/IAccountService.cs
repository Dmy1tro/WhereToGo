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

        Task<BaseResult> UnScribeFromEvent(int eventId, string userId);

        Task<BaseResult> RateEvent(RatingViewModel ratingView, string userId);

        Task<BaseResult> AddComment(CommentViewModel comment, string userId);

        Task<BaseResult> UpdateComment(CommentViewModel comment, string userId);

        Task<BaseResult> RemoveComment(int commentId, string userId);

        Task<BaseResult> InviteUserToEvent(Invitation invitation, string userId);
    }
}
