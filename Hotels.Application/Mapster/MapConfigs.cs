using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Application.RoomModule.CreateFeature.Command;
using Hotels.Application.RoomModule.UpdateFeature.Command;
using Hotels.Domain.Entities.Identity;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos.AuthModule;
using Hotels.Shared.Dtos.RoomModule;
using Mapster;

namespace Hotels.Application.Mapster
{
    public class MapConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Auth Module
            config.NewConfig<AppUser, UserToReturnDto>();

            //Room Module
            config.NewConfig<CreateRoomCommand, Room>();
            config.NewConfig<UpdateRoomCommand, Room>();
            config.NewConfig<Room, RoomToReturnDto>()
                .Map(dest => dest.RoomTypeId, src => src.RoomType)
                .Map(dest => dest.RoomTypeName, src => src.RoomType.ToString());
            config.NewConfig<Reservation, RoomReservationDto>()
                .Map(dest => dest.StatusId, src => src.Status)
                .Map(dest => dest.StatusName, src => src.Status.ToString());

            //Reservation Modul
            config.NewConfig<CreateReservationCommand, Reservation>()
                .Map(dest => dest.Status, src => ReservationStatus.Confirmed);

        }
    }
}
