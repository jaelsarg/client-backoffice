using Howestprime.Movies.ApiClient.Clients;
using Howestprime.Movies.ApiClient.Core;
using Howestprime.Movies.ApiClient.Requests;
using Howestprime.Movies.ApiClient.Responses;

namespace Howestprime.Backoffice.Services;

public class MovieEventService(IMovieEventsApiClient movieEventsApiClient) : IMovieEventService
{
    public async Task<IList<MovieEvent>> FindMovieEventsForMonth(int month, int year)
    {
        FindMovieEventsForMonthRequest request = new FindMovieEventsForMonthRequest
        {
            Month = month,
            Year = year
        };

        ApiResult<IReadOnlyList<MovieEvent>> result = await movieEventsApiClient.FindMovieEventsForMonthAsync(request);

        return result.Value?.ToList() ?? [];
    }

    public async Task<string> ScheduleMovieEvent(ScheduleMovieEventRequest eventRequest)
    {
        Console.WriteLine($"Scheduling event: MovieId={eventRequest.MovieId}, RoomId={eventRequest.RoomId}, Showtime={eventRequest.Showtime}");

        ScheduleMovieEventRequest request = new ScheduleMovieEventRequest
        {
            MovieId = eventRequest.MovieId,
            RoomId = eventRequest.RoomId,
            Showtime = eventRequest.Showtime
        };

        var result = await movieEventsApiClient.ScheduleMovieEventAsync(request);

        Console.WriteLine(result);

        return "Event scheduled successfully";
    }
}