using Hotels.Domain.Entities.Identity;
using Hotels.Domain.Entities.Room;
using Microsoft.AspNetCore.Identity;

namespace Hotels.Infrastructure.Persistence.Data.Seeds
{
    public static class DataSeeding
    {
        public static Dictionary<string, object> CreatedUser()
        {
            var User = new AppUser()
            {
                FullName = "Admin",
                Email = "Admin@hotels.com",
                isActive = true,
                CreatedAt = DateTime.UtcNow,
                UserName = "Admin_Hotels22"
            };
            var Password = "@Admin@1111";
            return new Dictionary<string, object>
            {
                {"User", User },
                {"Password", Password },
            };
        }
        public static ICollection<Room> CreateRooms()
        {
            var rooms = new List<Room>()
            {
                new Room()
                {
                    RoomNumber = 101,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 80
                },
                new Room()
                {
                    RoomNumber = 102,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 85
                },
                new Room()
                {
                    RoomNumber = 103,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 90
                },
                new Room()
                {
                    RoomNumber = 104,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 95
                },
                new Room()
                {
                    RoomNumber = 105,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 100
                },
                new Room()
                {
                    RoomNumber = 106,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 85
                },
                new Room()
                {
                    RoomNumber = 107,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 90
                },
                new Room()
                {
                    RoomNumber = 108,
                    RoomType = RoomType.Single,
                    IsAvailable = true,
                    PricePerNight = 100
                },

                new Room()
                {
                    RoomNumber = 201,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 120
                },
                new Room()
                {
                    RoomNumber = 202,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 125
                },
                new Room()
                {
                    RoomNumber = 203,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 130
                },
                new Room()
                {
                    RoomNumber = 204,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 135
                },
                new Room()
                {
                    RoomNumber = 205,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 140
                },
                new Room()
                {
                    RoomNumber = 206,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 125
                },
                new Room()
                {
                    RoomNumber = 207,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 135
                },
                new Room()
                {
                    RoomNumber = 208,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 145
                },
                new Room()
                {
                    RoomNumber = 209,
                    RoomType = RoomType.Double,
                    IsAvailable = true,
                    PricePerNight = 150
                },

                new Room()
                {
                    RoomNumber = 301,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 220
                },
                new Room()
                {
                    RoomNumber = 302,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 240
                },
                new Room()
                {
                    RoomNumber = 303,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 260
                },
                new Room()
                {
                    RoomNumber = 304,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 280
                },
                new Room()
                {
                    RoomNumber = 305,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 300
                },
                new Room()
                {
                    RoomNumber = 306,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 250
                },
                new Room()
                {
                    RoomNumber = 307,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 275
                },
                new Room()
                {
                    RoomNumber = 308,
                    RoomType = RoomType.Suite,
                    IsAvailable = true,
                    PricePerNight = 320
                }
            };

            return rooms;
        }
    }
}
