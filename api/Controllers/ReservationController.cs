using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Model;

using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDB reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.book_id }, reservation);
        }
    }
}