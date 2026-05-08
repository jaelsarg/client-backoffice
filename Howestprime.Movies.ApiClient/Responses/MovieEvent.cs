namespace Howestprime.Movies.ApiClient.Responses;

public sealed class MovieEvent
{
    public Guid Id { get; set; }

    public Movie Movie { get; set; } = new();

    public Room Room { get; set; } = new();

    public DateTime Showtime { get; set; }

    public int Capacity { get; set; }
}
