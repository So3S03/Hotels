using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Application.RoomModule.CreateFeature.Command;
using Hotels.Application.RoomModule.UpdateFeature.Command;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Identity;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos.AuthModule;
using Hotels.Shared.Dtos.LogsModule;
using Hotels.Shared.Dtos.ReportModule;
using Hotels.Shared.Dtos.ReservationModule;
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
            config.NewConfig<CreateRoomCommand, CreateRoomDto>()
                .Map(dest => dest.RoomType, src => ((RoomType)src.RoomType).ToString());
            config.NewConfig<UpdateRoomCommand, ModifyRoomDto>()
                .Map(dest => dest.RoomType, src => ((RoomType)src.RoomType).ToString());
            config.NewConfig<Reservation, RoomReservationDto>()
                .Map(dest => dest.StatusId, src => src.Status)
                .Map(dest => dest.StatusName, src => src.Status.ToString());

            //Reservation Modul
            config.NewConfig<CreateReservationCommand, Reservation>()
                .Map(dest => dest.Status, src => ReservationStatus.Confirmed);
            config.NewConfig<CreateReservationCommand, CreateReservationDto>()
                .Map(dest => dest.Status, src => ReservationStatus.Confirmed.ToString());

            config.NewConfig<Reservation, ReservationToReturnDto>()
                .Map(dest => dest.StatusName, src => src.Status.ToString())
                .Map(dest => dest.StatusId, src => src.Status)
                .Map(dest => dest.RoomNumber, src => src.Room.RoomNumber);

            //AuditLogModule
            config.NewConfig<AuditLog, LogToReturnDto>()
                .Map(dest => dest.ActionTypeName, src => src.ActionType.ToString())
                .Map(dest => dest.ActionTypeId, src => src.ActionType);

            //Reports Module
            config.NewConfig<Room, TopNonCancelledRoomToReturnDto>()
                .Map(dest => dest.RoomType, src => src.RoomType.ToString())
                .Map(dest => dest.ReservationCount, src => src.Reservations.Count)
                .Map(dest => dest.TotalRevenue, src => src.Reservations.Sum(r => r.TotalAmount));

        }
    }
}
