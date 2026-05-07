using Howestprime.Backoffice.Components.Planning;
using Howestprime.Backoffice.Services;
using Howestprime.Backoffice.ViewModels.Planning;
using Howestprime.Movies.ApiClient.Requests;
using Howestprime.Movies.ApiClient.Responses;
using Microsoft.AspNetCore.Components;

namespace Howestprime.Backoffice.Components.Pages;

public partial class Planning
{
    private IList<RoomViewModel> Rooms = [];
    private IList<MovieViewModel> Movies = [];
    private IList<EventViewModel> Events = [];
    public IList<DayViewModel> DaysInMonth = [];

    public int month = DateTime.Now.Month;
    public string monthName = DateTime.Now.ToString("MMMM");
    public int year = DateTime.Now.Year;
    private DayViewModel selectedDay;

    [Inject]
    private IRoomService RoomService { get; set; } = default!;
    [Inject]
    private IMovieService MovieService { get; set; } = default!;
    [Inject]
    private IMovieEventService MovieEventService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadRooms();
        await LoadMovies();
        await LoadMovieEvents();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RenderCalendar();
        }
    }

    protected async Task LoadRooms()
    {
        IList<Room> RoomResponses = await RoomService.GetRooms();
        Rooms = RoomResponses.Select(r => new RoomViewModel
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();
    }

    protected async Task LoadMovies()
    {
        IList<Movie> MovieResponses = await MovieService.GetMovies(userRole: "Admin");
        Movies = MovieResponses.Select(m => new MovieViewModel
        {
            Id = m.Id,
            Title = m.Title
        }).ToList();
    }

    protected async Task LoadMovieEvents()
    {
        IList<MovieEvent> MovieEventResponses = await MovieEventService.FindMovieEventsForMonth(DateTime.Now.Month, DateTime.Now.Year);
        Events = MovieEventResponses.Select(e => new EventViewModel
        {
            MovieId = e.Movie.Id.ToString(),
            RoomId = e.Room.Id.ToString(),
            ShowTime = e.Showtime
        }).ToList();
    }

    protected async Task RenderCalendar()
    {
        RenderCalendarDays(month, year);
    }

    protected async Task RenderCalendarDays(int month, int year)
    {
        // Show movie events in day

        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        int daysInMonth = DateTime.DaysInMonth(year, month);

        DaysInMonth.Clear();

        for (int i = 0; i < firstDayOfWeek; i++)
        {
            DaysInMonth.Add(new DayViewModel
            {
               IsEmpty = true,
               IsDisabled = false
            });
        }

        for (int i = 1; i <= daysInMonth; i++)
        {
            DaysInMonth.Add(new DayViewModel
            {
                Day = i,
                Date = new DateTime(year, month, i),
                IsEmpty = false,
                IsDisabled = new DateTime(year, month, i) < DateTime.Today,
                ShowEventModal = false
            });
        }

        StateHasChanged();
    }

    protected async Task OnMonthChange(int offset)
    {
        DateTime newDate = new DateTime(year, month, 1).AddMonths(offset);
        month = newDate.Month;
        monthName = newDate.ToString("MMMM");
        year = newDate.Year;
        await RenderCalendar();
    }

    protected async Task ShowEventModal(DayViewModel day)
    {
        selectedDay = day;
        selectedDay.ShowEventModal = true;
    }

    protected async Task HideEventModal()
    {
        selectedDay.ShowEventModal = false;
        selectedDay = null;
    }

    protected async Task SaveNewEvent()
    {
        // Get data from form
        // Create request
        // Send request
        // Load movie events
        // Render movie events
        // Hide modal
    }
}