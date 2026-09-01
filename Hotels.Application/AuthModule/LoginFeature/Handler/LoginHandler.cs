using Hotels.Application.AuthModule.LoginFeature.Command;
using Hotels.Domain.Entities.Identity;
using Hotels.Shared.Dtos.AuthModule._Common;
using Hotels.Shared.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotels.Application.AuthModule.LoginFeature.Handler
{
    public class LoginHandler(UserManager<AppUser> _userManager, IConfiguration _configuration) : IRequestHandler<LoginCommand, SignInStatusDto>
    {
        public async Task<SignInStatusDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { Email: null or "" } => throw new BadRequest400Exception("Invalid Email"),
                { Password: null or "" } => throw new BadRequest400Exception("Invalid Password"),
                _ => request
            };
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null || !await _userManager.CheckPasswordAsync(user, request.Password)) throw new BadRequest400Exception("Invalid Email or Password");
            if(!user.isActive) throw new BadRequest400Exception("User is not active contact administrator");
            var Obj = new SignInStatusDto()
            {
                Succeeded = true,
                Message = "User Logged In Successfully",
                Token = await GenerateJwtToken(user)
            };
            return Obj;
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id),
                new Claim("UserName", user.FullName),
                new Claim("Email", user.Email!),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(r => new Claim("Role", r)));
            var jwtConfigs = _configuration.GetSection("JwtConfigs");
            var secretKey = jwtConfigs.GetValue<string>("SecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var signinCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtConfigs.GetValue<string>("Issuer"),
                audience: jwtConfigs.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtConfigs.GetValue<int>("TokenExpirationInMinutes")),
                signingCredentials: signinCred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
