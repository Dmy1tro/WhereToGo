using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Services;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
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
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("not valid model");

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
        public async Task<ActionResult> RegisterUser(RegisterUserViewModel registerModel)
        {
            if (!ModelState.IsValid || !registerModel.AcceptRules)
                return BadRequest("model not valid");

            var registerUserResult = await signInService.RegisterUser(registerModel);

            if (!(registerUserResult.IsValid))
                return BadRequest(registerUserResult.Errors);

            if (registerModel.CreateCompany)
            {
                return Ok(
                    new
                    {
                        passwordHash = registerUserResult.User.PasswordHash
                    });
            }

            return NoContent();
        }

        [HttpPost("registerCompany")]
        public async Task<ActionResult> RegisterCompany(RegisterCompanyViewModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("model not valid");

            var registerCompanyResult = await signInService.RegisterCompany(registerModel);

            if (!registerCompanyResult.IsValid)
                return BadRequest(registerCompanyResult.Errors);

            return Ok();
        }
    }
}