using Hotels.Application.RoomModule.GetAllRoomsFeature.Query;
using Hotels.Application.Specifications._Common;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Room;
using System.Linq.Expressions;

namespace Hotels.Application.Specifications.RoomSpecs
{
    internal class RoomsListSpec : BaseSpecification<Room>
    {
        public RoomsListSpec(GetAllRoomsQuery parameter, bool isPagination = true): base(
            CritriaCreator.CreateCriteria<Room>(
                    CreateAvailabiltyCriteria(parameter.isAvailable)!,
                    CreateRoomTypeCriteria(parameter.Type)!,
                    CreatePriceRangeCriteria(parameter.StartPrice, parameter.EndPrice)!
                )
            )
        {
            addIncludes(r => r.Reservations);
            if(parameter.PageNum > 0 && parameter.PageSize > 0 && isPagination)
            {
                Pagination(parameter.PageNum, parameter.PageSize);
            }
        }

        private static Expression<Func<Room, bool>>? CreateAvailabiltyCriteria(bool? isAvailable)
        {
            if (isAvailable is null) return null;
            return R => R.IsAvailable == isAvailable;
        }

        private static Expression<Func<Room, bool>>? CreateRoomTypeCriteria(RoomType? type)
        {
            if (type is null) return null;
            return R => R.RoomType == type;
        }

        private static Expression<Func<Room, bool>>? CreatePriceRangeCriteria(decimal? startPrice, decimal? endPrice)
        {
            if (startPrice is null || endPrice is null) return null;
            return R => R.PricePerNight >= startPrice && R.PricePerNight <= endPrice;
        }
    }
}
