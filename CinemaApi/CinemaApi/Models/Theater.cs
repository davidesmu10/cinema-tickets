namespace CinemaApi.Models;


public class Theater
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Rows { get; set; }
    public int SeatsPerRow { get; set; }
}