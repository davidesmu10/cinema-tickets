namespace CinemaApi.Models;


public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string PosterUrl { get; set; } = default!;
    public int DurationMinutes { get; set; }
    public string? Rating { get; set; }
}