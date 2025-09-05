namespace CinemaApi.Models;


public class Reservation
{
    public int Id { get; set; }
    public string Code { get; set; } = default!; // p.ej. RSV-20240903-...
    public int ScreeningId { get; set; }
    public List<SeatSelection> Seats { get; set; } = new();
    public decimal Total { get; set; }
    public string Status { get; set; } = "Confirmed"; // Confirmed | Cancelled
    public DateTime CreatedAt { get; set; }
    public string CustomerName { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
}