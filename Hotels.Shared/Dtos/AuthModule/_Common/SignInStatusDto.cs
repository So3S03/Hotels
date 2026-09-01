using Hotels.Shared.Dtos._Common;

namespace Hotels.Shared.Dtos.AuthModule._Common
{
    public class SignInStatusDto : ActionStatusDto
    {
        public string Token { get; set; }
    }
}
