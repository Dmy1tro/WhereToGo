using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Authentication;
using WhereToGoWebApi.Models.AccountViewModels;
using WhereToGoWebApi.Services.Interfaces;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.adminRole)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet("GetListOfUsers")]
        public async Task<ActionResult<IEnumerable<UserProfileViewModel>>> GetListOfUsers() =>
            Ok(await adminService.GetListOfUsers());

        [HttpGet("getUserProfile/{userId}")]
        public async Task<ActionResult<UserProfileViewModel>> GetUserProfile(string userId)
        {
            var userProfile = adminService.GetUserProfile(userId);

            return userProfile is null
                ? BadRequest($"User with id '{userId}' not found") as ActionResult
                : Ok(userProfile);
        }

        [HttpPost("removeComment")]
        public async Task<ActionResult> RemoveComment([FromBody] int commentId)
        {
            var result = await adminService.RemoveComment(commentId);

            return result.IsValid
                ? NoContent() as ActionResult
                : BadRequest(result.Errors);
        }
    }
}