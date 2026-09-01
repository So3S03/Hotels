using Hotels.Application.AuthModule.RegisterFeature.Command;
using Hotels.Domain.Entities.Identity;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Hotels.Application.AuthModule.RegisterFeature.Handler
{
    public class RegisterHandler(UserManager<AppUser> userManager) : IRequestHandler<RegisterCommand, ActionStatusDto>
    {
        private const string EmailPaattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}";
        public async Task<ActionStatusDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //Check On Data
            _ = request switch
            {
                { FullName: "" or null } => throw new BadRequest400Exception("Invalid Full Name"),
                { Email: "" or null } => throw new BadRequest400Exception("Invalid Email"),
                { Email: var email } when !Regex.IsMatch(email, EmailPaattern) => throw new BadRequest400Exception("Invalid Email Format"),
                { UserName: "" or null } => throw new BadRequest400Exception("Invalid User Name"),
                { PhoneNumber: "" or null } => throw new BadRequest400Exception("Invalid Phone Number"),
                { Password: "" or null } => throw new BadRequest400Exception("Invalid Password"),
                { Password: var password } when !Regex.IsMatch(password, passwordPattern) => throw new BadRequest400Exception("Invalid Password Format"),
                _ => request
            };
            var user = await userManager.FindByEmailAsync(request.Email);
            if(user is not null) throw new Conflict409Exception("User Already Exists");
            user = new AppUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                CreatedAt = DateTime.UtcNow,
                isActive = false,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new Exception("Something Went Wrong!");
            var Obj = new ActionStatusDto()
            {
                Succeeded = true,
                Message = "User Created Successfully!"
            };
            return Obj;
        }
    }
}
