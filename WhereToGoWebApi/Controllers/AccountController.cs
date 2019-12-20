using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Authentication;
using WhereToGoWebApi.Common.Extensions;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("editProfile")]
        public async Task<IActionResult> EditProfile(UserProfileViewModel model)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.EditProfile(model, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [Authorize(Roles = AppRoles.organaizerRole)]
        [HttpPost("editOrganizerProfile")]
        public async Task<IActionResult> EditOrganaizerProfile(OrganizerProfileViewModel model)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.EditOrganaizerInfo(model, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordModel)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.ChangePassword(changePasswordModel, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("subscribeOnEvent/{eventId}")]
        public async Task<ActionResult> SubscribeOnEvent([FromBody] int eventId)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.SubscribeOnEvent(eventId, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("unScribeOnEvent/{eventId}")]
        public async Task<ActionResult> UnScribeOnEvent([FromBody] int eventId)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.UnScribeFromEvent(eventId, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("rateEvent")]
        public async Task<ActionResult> RateEvent(RatingViewModel model)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.RateEvent(model, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("addComment")]
        public async Task<ActionResult> AddComment(CommentViewModel comment)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.AddComment(comment, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("updateComment")]
        public async Task<ActionResult> UpdateComment(CommentViewModel comment)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.UpdateComment(comment, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("removeComment")]
        public async Task<ActionResult> RemoveComment([FromBody] int commentId)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.RemoveComment(commentId, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }

        [HttpPost("inviteUserToEvent")]
        public async Task<ActionResult> InviteUserToEvent(Invitation invitation)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.InviteUserToEvent(invitation, userId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }
    }
}