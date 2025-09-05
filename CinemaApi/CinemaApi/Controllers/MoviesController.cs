using CinemaApi.InMemory;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        // GET: api/movies
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll()
        {
            var result = StaticStore.Movies.Select(m => new
            {
                m.Id,
                m.Title,
                m.Description,
                m.PosterUrl,
                m.DurationMinutes,
                m.Rating,
                NextScreenings = StaticStore.Screenings
                    .Where(s => s.MovieId == m.Id)
                    .Select(s => new
                    {
                        s.Id,
                        s.StartTime,
                        s.Price,
                        s.TheaterId
                    })
            });

            return Ok(result);
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public ActionResult<object> GetById(int id)
        {
            var movie = StaticStore.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var result = new
            {
                movie.Id,
                movie.Title,
                movie.Description,
                movie.PosterUrl,
                movie.DurationMinutes,
                movie.Rating,
                NextScreenings = StaticStore.Screenings
                    .Where(s => s.MovieId == movie.Id)
                    .Select(s => new
                    {
                        s.Id,
                        s.StartTime,
                        s.Price,
                        s.TheaterId
                    })
            };

            return Ok(result);
        }

        // GET: api/movies/{id}/showtimes
        [HttpGet("{id}/showtimes")]
        public ActionResult<IEnumerable<object>> GetShowtimes(int id)
        {
            var movie = StaticStore.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            var showtimes = StaticStore.Screenings
                .Where(s => s.MovieId == id)
                .Select(s => new
                {
                    s.Id,
                    s.StartTime,
                    s.Price,
                    s.TheaterId
                });

            return Ok(showtimes);
        }


        [HttpGet("{id}/seats")]
        public ActionResult<object> GetSeats(int id)
        {
            var screening = StaticStore.Screenings.FirstOrDefault(s => s.Id == id);
            if (screening == null)
                return NotFound(new { message = "Showtime not found" });

            var theater = StaticStore.Theaters.First(t => t.Id == screening.TheaterId);

            var seats = StaticStore.GenerateAllSeats(theater)
                .Select(seatId => new
                {
                    Id = seatId,
                    Row = seatId[0].ToString(),
                    Number = seatId.Substring(1),
                    IsReserved = StaticStore.ReservedByScreening[screening.Id].Contains(seatId)
                });

            return Ok(seats);
        }
    }
}
