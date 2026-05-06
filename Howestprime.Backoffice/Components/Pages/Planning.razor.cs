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
    //private IList<DayViewModel> Days = [];

    string month = DateTime.Now.ToString("MMMM");
    int year = DateTime.Now.Year;
    DateTime selectedDate;
    bool isShowingEventModal = false;
    private MonthNavigator MonthNavigator { get; set; }
    private CalendarGrid CalendarGrid { get; set; }




    [Inject]
    private IMovieEventService MovieEventService { get; set; } = default!;
    [Inject]
    private IRoomService RoomService { get; set; } = default!;
    [Inject]
    private IMovieService MovieService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadRooms();
        await LoadMovies();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RenderCalendar();
        }
    }

    protected async Task RenderCalendar()
    {
        MonthNavigator.UpdateMonthYearHeader(month, year);
        await CalendarGrid.RenderCalendarDays(month, year);
    }

    protected async Task LoadMovies()
    {
        IList<Movie> MovieResponses = await MovieService.GetMovies(userRole: "Admin");
        Console.WriteLine($"Fetched {MovieResponses.Count} movies from API.");
        Movies = MovieResponses.Select(m => new MovieViewModel
        {
            Id = m.Id,
            Title = m.Title
        }).ToList();
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

    protected async Task OnMonthChange(DateTime newDate)
    {
        month = newDate.ToString("MMMM");
        year = newDate.Year;
        await RenderCalendar();
    }

    protected async Task ShowEventModal(DateTime date)
    {
        selectedDate = date;
        isShowingEventModal = true;
    }

    protected void CloseEventModal()
    {
        isShowingEventModal = false;
    }

    protected async Task SaveNewEvent(EventViewModel eventData)
    {
        ScheduleMovieEventRequest request = new ScheduleMovieEventRequest
        {
            MovieId = Guid.Parse(eventData.MovieId),
            RoomId = Guid.Parse(eventData.RoomId),
            Showtime = eventData.ShowTime
        };

        await MovieEventService.ScheduleMovieEvent(request);

        CloseEventModal();
    }
}