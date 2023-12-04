
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AccountRepository(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<string> SignInAsync(SignIn model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

                if (user == null || !passwordCheck)
                {
                    return string.Empty;
                }

                var authClaims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", model.Username!),
                };

                var userRole = await _userManager.GetRolesAsync(user);
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddMinutes(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IdentityResult> SignUpAsync(SignUp model, bool IsAdmin)
        {
            try
            {

                var user = _mapper.Map<ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var rolesToAdd = new List<string> { AppRole.Customer };

                    if (IsAdmin)
                    {
                        rolesToAdd.Add(AppRole.Admin);
                    }

                    foreach (var role in rolesToAdd)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }

                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                return result;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }
}
