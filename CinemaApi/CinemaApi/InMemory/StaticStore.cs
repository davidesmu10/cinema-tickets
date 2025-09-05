using CinemaApi.Models;
using System.Globalization;

namespace CinemaApi.InMemory;

public static class StaticStore
{
    public static readonly List<Movie> Movies = new();
    public static readonly List<Theater> Theaters = new();
    public static readonly List<Screening> Screenings = new();
    public static readonly List<Reservation> Reservations = new();

    // Seats reservados por función: key = screeningId, value = HashSet de ids "A1", "B5", etc.
    public static readonly Dictionary<int, HashSet<string>> ReservedByScreening = new();

    private static int _movieSeq = 1;
    private static int _theaterSeq = 1;
    private static int _screeningSeq = 1;
    private static int _reservationSeq = 1;

    public static void Seed()
    {
        if (Movies.Any()) return; // evita seed duplicado

        Movies.AddRange(new[]
        {
            new Movie{ Id = _movieSeq++, Title = "Galaxias 9", Description = "Sci-Fi épica.", PosterUrl = "https://picsum.photos/300/450?1", DurationMinutes = 124, Rating = "PG-13" },
            new Movie{ Id = _movieSeq++, Title = "Risas y Llantos", Description = "Comedia dramática.", PosterUrl = "https://picsum.photos/300/450?2", DurationMinutes = 108, Rating = "B-15" },
            new Movie{ Id = _movieSeq++, Title = "La Sombra", Description = "Thriller oscuro.", PosterUrl = "https://picsum.photos/300/450?3", DurationMinutes = 132, Rating = "R" }
        });

        Theaters.AddRange(new[]
        {
            new Theater{ Id = _theaterSeq++, Name = "Sala 1", Rows = 8, SeatsPerRow = 12 },
            new Theater{ Id = _theaterSeq++, Name = "Sala 2", Rows = 10, SeatsPerRow = 10 }
        });

        // Crear funciones para los próximos 7 días
        var today = DateTime.Today;
        var rand = new Random(11);
        foreach (var movie in Movies)
        {
            for (int d = 0; d < 7; d++)
            {
                var date = today.AddDays(d);
                // tres horarios por día
                foreach (var hour in new[] { 14, 17, 20 })
                {
                    var theater = Theaters[(d + hour) % Theaters.Count];
                    var s = new Screening
                    {
                        Id = _screeningSeq++,
                        MovieId = movie.Id,
                        TheaterId = theater.Id,
                        StartTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0),
                        Price = 24000 + (hour * 10)
                    };
                    Screenings.Add(s);
                    ReservedByScreening[s.Id] = new HashSet<string>();

                    // marcar algunos asientos ya reservados de forma aleatoria
                    var reserved = ReservedByScreening[s.Id];
                    var toReserve = rand.Next(5, 18);
                    foreach (var seat in GenerateAllSeats(theater))
                    {
                        if (reserved.Count >= toReserve) break;
                        if (rand.NextDouble() < 0.08) reserved.Add(seat);
                    }
                }
            }
        }
    }

    public static IEnumerable<string> GenerateAllSeats(Theater theater)
    {
        for (int r = 0; r < theater.Rows; r++)
        {
            var rowLetter = (char)('A' + r);
            for (int n = 1; n <= theater.SeatsPerRow; n++)
            {
                yield return $"{rowLetter}{n}";
            }
        }
    }

    public static Reservation CreateReservation(int screeningId, IEnumerable<string> seatIds, string name, string email)
    {
        var screening = Screenings.First(s => s.Id == screeningId);
        var theater = Theaters.First(t => t.Id == screening.TheaterId);
        var allSeats = new HashSet<string>(GenerateAllSeats(theater));
        var reserved = ReservedByScreening[screeningId];

        // Validar que existan y estén libres
        foreach (var sid in seatIds)
        {
            if (!allSeats.Contains(sid)) throw new InvalidOperationException($"Seat inválido: {sid}");
            if (reserved.Contains(sid)) throw new InvalidOperationException($"Seat ocupado: {sid}");
        }

        // Marcar como reservados
        foreach (var sid in seatIds) reserved.Add(sid);

        var total = seatIds.Count() * screening.Price;
        var code = $"RSV-{DateTime.UtcNow:yyyyMMddHHmmss}-{_reservationSeq}";
        var res = new Reservation
        {
            Id = _reservationSeq++,
            Code = code,
            ScreeningId = screeningId,
            Seats = seatIds.Select(x => new SeatSelection { SeatId = x }).ToList(),
            Total = total,
            Status = "Confirmed",
            CreatedAt = DateTime.UtcNow,
            CustomerEmail = email,
            CustomerName = name
        };
        Reservations.Add(res);
        return res;
    }

    public static void CancelReservation(Reservation res)
    {
        if (res.Status == "Cancelled") return;
        var reserved = ReservedByScreening[res.ScreeningId];
        foreach (var s in res.Seats) reserved.Remove(s.SeatId);
        res.Status = "Cancelled";
    }
}