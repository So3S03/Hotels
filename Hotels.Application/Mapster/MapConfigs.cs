using Hotels.Domain.Entities.Identity;
using Hotels.Shared.Dtos.AuthModule;
using Mapster;

namespace Hotels.Application.Mapster
{
    public class MapConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AppUser, UserToReturnDto>();
        }
    }
}
