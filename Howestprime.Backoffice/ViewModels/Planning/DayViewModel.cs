namespace Howestprime.Backoffice.ViewModels.Planning;

public class DayViewModel
{
    public int? Day { get; set; }
    public DateTime Date { get; set; }
    public bool IsEmpty { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
    public bool ShowEventModal { get; set; } = false;
    public IList<EventViewModel>? MovieEvents { get; set; } = new List<EventViewModel>();
}