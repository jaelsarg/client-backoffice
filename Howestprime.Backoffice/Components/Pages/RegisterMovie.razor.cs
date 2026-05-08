using Howestprime.Backoffice.Services;
using Howestprime.Backoffice.ViewModels.Movie;
using Howestprime.Movies.ApiClient.Requests;
using Microsoft.AspNetCore.Components;

namespace Howestprime.Backoffice.Components.Pages;

public partial class RegisterMovie
{
    private string posterUrl = string.Empty;
    private (string ErrorType, string ErrorMessage) message = (ErrorType: string.Empty, ErrorMessage: string.Empty);

    [Inject]
    public IMovieService MovieService { get; set; } = default!;

    protected async Task RegisterNewMovie(RegisterMovieViewModel newMovie)
    {
        RegisterMovieRequest request = new RegisterMovieRequest
        {
            Title = newMovie.Title,
            Description = newMovie.Description,
            ReleaseYear = newMovie.ReleaseYear,
            Duration = newMovie.Duration,
            Genres = newMovie.Genre.Split(',').Select(g => g.Trim()).ToList(),
            Actors = newMovie.Actors.Split(',').Select(a => a.Trim()).ToList(),
            AgeRating = newMovie.AgeRating,
            PosterUrl = newMovie.PosterUrl
        };

        string movieId = await MovieService.RegisterMovie(request);

        if (string.IsNullOrEmpty(movieId))
        {
            message = (ErrorType: "error-message", ErrorMessage: "Failed to register movie.");
            return;
        }

        message = (ErrorType: "success-message", ErrorMessage: $"{newMovie.Title} registered successfully!");

        Console.WriteLine($"New movie registered with ID: {movieId}");
    }

    protected void PosterUrlChanged(string url)
    {
        posterUrl = url;
    }
}