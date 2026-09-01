using Hotels.Shared.Dtos.AuthModule._Common;
using MediatR;

namespace Hotels.Application.AuthModule.LoginFeature.Command
{
    public class LoginCommand : IRequest<SignInStatusDto>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
