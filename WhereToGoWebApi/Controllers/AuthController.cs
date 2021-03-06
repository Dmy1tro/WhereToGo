﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Authentication;
using WhereToGoWebApi.Common.Extensions;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Services;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISignInService signInService;

        public AuthController(ISignInService signInService)
        {
            this.signInService = signInService;
        }

        [HttpGet]
        public ActionResult Get() =>
            Ok();

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var loginResult = await signInService.LoginUser(loginModel);

            if (!loginResult.IsValid)
                return BadRequest(loginResult.Errors);

            return Ok(
                new
                {
                    token = loginResult.Token,
                    roles = loginResult.Roles.ToArray(),
                    name = loginResult.Name
                });
        }

        [HttpPost("registerUser")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserViewModel registerModel)
        {
            var registerUserResult = await signInService.RegisterUser(registerModel);

            if (!(registerUserResult.IsValid))
                return BadRequest(registerUserResult.Errors);

            var loginResult = await signInService.LoginUser(new LoginViewModel { Email = registerModel.Email, Password = registerModel.Password, RememberMe = false });

            return Ok(
                new 
                {
                    token = loginResult.Token,
                    roles = loginResult.Roles.ToArray(),
                    name = loginResult.Name
                });
        }

        [Authorize]
        [HttpPost("registerCompany")]
        public async Task<ActionResult> RegisterCompany(RegisterOrganaizerViewModel registerModel)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);

            var registerCompanyResult = await signInService.RegisterCompany(registerModel, userId);

            if (!registerCompanyResult.IsValid)
                return BadRequest(registerCompanyResult.Errors);

            return NoContent();
        }
    }
}