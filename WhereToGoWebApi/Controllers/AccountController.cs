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
    [ApiController]
    [Authorize]
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
            if (!ModelState.IsValid)
                return BadRequest("Model not valid!");

            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.EditProfile(model, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [Authorize(Roles = AppRoles.organaizerRole)]
        [HttpPost("editOrganizerProfile")]
        public async Task<IActionResult> EditOrganaizerProfile(OrganizerProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid!");

            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.EditOrganaizerInfo(model, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid!");

            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.ChangePassword(changePasswordModel, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPost("subscribeOnEvent/{eventId}")]
        public async Task<ActionResult> SubscribeOnEvent([FromRoute] int eventId)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);
            var result = await accountService.SubscribeOnEvent(eventId, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

    }
}