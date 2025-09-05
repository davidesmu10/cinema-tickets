namespace CinemaApi.Models;


public class Screening
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int TheaterId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }
}