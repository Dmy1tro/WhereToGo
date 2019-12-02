using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Extensions;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("editProfile")]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid!");

            var currentUserName = User.Claims.GetUserName(ClaimsIdentity.DefaultNameClaimType);
            var result = await accountService.EditProfile(model, currentUserName);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid!");

            var currentUserName = User.Claims.GetUserName(ClaimsIdentity.DefaultNameClaimType);
            var result = await accountService.ChangePassword(changePasswordModel, currentUserName);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}