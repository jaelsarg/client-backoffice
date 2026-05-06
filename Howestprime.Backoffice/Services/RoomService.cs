using Howestprime.Movies.ApiClient.Responses;

namespace Howestprime.Backoffice.Services;

public class RoomService : IRoomService
{
    IList<Room> rooms = new List<Room>
    {
        new Room { Id = Guid.Parse("019d059e-d220-71db-8a1a-ec7569492999"), Name = "Blue Room", Capacity = 100 },
        new Room { Id = Guid.Parse("019d059e-d220-75fe-b936-0a97cd75216e"), Name = "Yellow Room", Capacity = 80 }
    };

    public Task<IList<Room>> GetRooms()
    {
        return Task.FromResult(rooms);
    }
}