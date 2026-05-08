using Howestprime.Movies.ApiClient.Clients;
using Howestprime.Movies.ApiClient.Core;
using Howestprime.Movies.ApiClient.Requests;
using Howestprime.Movies.ApiClient.Responses;

namespace Howestprime.Backoffice.Services;

public class MovieService(IMovieCatalogApiClient movieCatalogApiClient) : IMovieService
{
    public async Task<List<Movie>> GetMovies(string? title = null, string? genre = null, string? userRole = null)
    {
        SearchMovieCatalogRequest request = new SearchMovieCatalogRequest
        {
            UserRole = userRole,
            Title = title,
            Genres = genre   
        };

        ApiResult<MovieCollection> result = await movieCatalogApiClient.SearchMovieCatalogAsync(request);

        return result.Value?.Data.ToList() ?? [];

    }

    public async Task<string> RegisterMovie(RegisterMovieRequest movieRequest)
    {
        RegisterMovieRequest request = new RegisterMovieRequest
        {
            Title = movieRequest.Title,
            Description = movieRequest.Description,
            ReleaseYear = movieRequest.ReleaseYear,
            Duration = movieRequest.Duration,
            Genres = movieRequest.Genres,
            Actors = movieRequest.Actors,
            AgeRating = movieRequest.AgeRating,
            PosterUrl = movieRequest.PosterUrl
        };

        var result = await movieCatalogApiClient.RegisterMovieAsync(request);

        return result.Value?.ToString() ?? string.Empty;
    }
}