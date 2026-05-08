namespace Howestprime.Movies.ApiClient.Requests;

public sealed class ScheduleMovieEventRequest
{
    public Guid MovieId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime RequestedShowtime { get; set; }
}
