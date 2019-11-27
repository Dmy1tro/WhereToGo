using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhereToGoWebApi.Common.Authentication;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.AuthResults;
using WhereToGoWebApi.TokenSettings;

namespace WhereToGoWebApi.Services
{
    public class SignInService : ISignInService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEventDbRepository repository;
        private readonly JwtSettings jwtSettings;

        public SignInService(UserManager<User> userManager, 
                             RoleManager<IdentityRole> roleManager,
                             IEventDbRepository repository, 
                             JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.repository = repository;
            this.jwtSettings = jwtSettings;
        }

        private async Task CheckRoleExists(string roleName)
        {
            if (!(await roleManager.RoleExistsAsync(roleName)))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<LoginResult> LoginUser(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !(await userManager.CheckPasswordAsync(user, model.Password)))
                return new LoginResult("login or password is wrong");

            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x));

            var claims = new List<Claim>(roleClaims)
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
            int expiryInMinutes = int.Parse(jwtSettings.ExpiryInMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Site,
                audience: jwtSettings.Site,
                expires: DateTime.Now.AddMinutes(model.RememberMe ? expiryInMinutes * 2 : expiryInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResult(user.UserName, roles, jwtToken);
        }

        public async Task<RegisterResult> RegisterUser(RegisterUserViewModel model)
        {
            var user = new User
            {
                UserName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new RegisterResult(result.Errors.Select(x => x.Description));

            await CheckRoleExists(AppRoles.userRole);

            await userManager.AddToRoleAsync(user, AppRoles.userRole);

            return new RegisterResult(user);
        }

        public async Task<RegisterResult> RegisterCompany(RegisterCompanyViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (!user.PasswordHash.Equals(model.PasswordHash))
                return new RegisterResult("Failed to confirm email");

            var organaizer = new Organizer
            {
                User = user,
                InstType = model.InstType,
                PlaceName = model.PlaceName,
                Position = model.Position,
                TelNumber = model.TelNumber
            };

            var complete = await repository.CreateAndSaveOrganaizerAsync(organaizer);

            if (!complete)
                return new RegisterResult("Failed save to DataBase");

            await CheckRoleExists(AppRoles.organaizerRole);

            await userManager.AddToRoleAsync(user, AppRoles.organaizerRole);

            return new RegisterResult();
        }
    }
}
