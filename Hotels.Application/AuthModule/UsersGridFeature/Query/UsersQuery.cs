using Hotels.Application._Common;
using Hotels.Shared.Dtos.AuthModule;

namespace Hotels.Application.AuthModule.UsersGridFeature.Query
{
    public class UsersQuery : CommonGridQuery<UserToReturnDto>
    {
        public string? Name { get; set; }
    }
}
