namespace Howestprime.Backoffice.ViewModels.Planning;

public class EventViewModel
{
    public string MovieId { get; set; } = string.Empty;
    public string? MovieTitle { get; set; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;
    public string? RoomName { get; set; } = string.Empty;
    public DateTimeOffset ShowTime { get; set; }
}