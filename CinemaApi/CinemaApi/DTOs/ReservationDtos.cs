namespace CinemaApi.DTOs;


public record CreateReservationDto(int ScreeningId, List<string> Seats, string CustomerName, string CustomerEmail);
public record ReservationResponse(string Code, string Status, decimal Total, int ScreeningId, IEnumerable<string> Seats);