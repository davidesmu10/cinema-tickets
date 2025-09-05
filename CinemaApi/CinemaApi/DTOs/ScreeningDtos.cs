namespace CinemaApi.DTOs;


public record SeatDto(string Id, string Row, int Number, bool Reserved);
public record ScreeningSummaryDto(int Id, int MovieId, int TheaterId, DateTime StartTime, decimal Price, string TheaterName);