using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.TokenSettings;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> manager;
        private readonly JwtSettings jwtSettings;
        public AuthController(UserManager<User> manager, JwtSettings jwtSettings)
        {
            this.manager = manager;
            this.jwtSettings = jwtSettings;
        }

        [HttpGet]
        public ActionResult Get() =>
            Ok();

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("not valid model");

            var user = await manager.FindByNameAsync(loginModel.Login);

            if (user is null || !(await manager.CheckPasswordAsync(user, loginModel.Password)))
                return BadRequest("login or password is wrong");

            var claims = new[]
            {
                new Claim("userId", user.Id)
            };

            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
            int expiryInMinutes = int.Parse(jwtSettings.ExpiryInMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Site,
                audience: jwtSettings.Site,
                expires: DateTime.UtcNow.AddMinutes(loginModel.RememberMe ? expiryInMinutes * 2 : expiryInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    name = loginModel.Login
                });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("model not valid");

            var user = new User
            {
                UserName = registerModel.Login,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await manager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(x => x.Description));

            return Ok();
        }
    }
}