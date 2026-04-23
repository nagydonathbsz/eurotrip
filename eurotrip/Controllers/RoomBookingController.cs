using eurotrip.Modell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eurotrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        EuroContext _context;

        public RoomBookingController(EuroContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "RoomBooking.Read")]
        [HttpGet]
        public async Task<IActionResult> GetBookingList()
        {
            var list = await _context.RoomBookings.ToListAsync();
            return Ok(list);
        }
        [Authorize(Policy = "RoomBooking.ReadMe")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyBookingList()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (email == null) return Unauthorized();
            var me = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (me == null) return Unauthorized();
            var list = await _context.RoomBookings
                .Include(rb => rb.Room)
                    .ThenInclude(r => r.Accommodation)
                        .ThenInclude(a => a.City)
                .Where(rb => rb.UserId == me.Id)
                .ToListAsync();
            return Ok(list);
        }
        [Authorize(Policy = "RoomBooking.ReadId")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingListById(int id)
        {
            RoomBooking? rb = await _context.RoomBookings.FirstOrDefaultAsync(m => m.Id == id);
            if (rb == null) return NotFound();
            return Ok(rb);
        }
        [Authorize(Policy = "RoomBooking.ReadStatus")]
        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetBookingListStatus(int id)
        {
            RoomBooking? rb = await _context.RoomBookings.FirstOrDefaultAsync(m => m.Id == id);
            if (rb == null) return NotFound();
            return Ok(rb.Status?.ToString());
        }

        [Authorize(Policy = "RoomBooking.Create")]
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] RoomBooking rb)
        {
            if (rb?.UserId == null || rb?.RoomId == null || !rb.CheckIn.HasValue || !rb.CheckOut.HasValue)
                return BadRequest("Missing data");

            var now = DateTime.UtcNow.Date;
            if (rb.CheckIn.Value.Date < now)
                return BadRequest("A foglalás nem lehet a múltban.");
            if (rb.CheckIn.Value.Date > now.AddMonths(6))
                return BadRequest("A foglalás legfeljebb 6 hónapra előre lehetséges.");

            var checkIn = rb.CheckIn.Value;
            var checkOut = rb.CheckOut.Value;

            bool overlap = await _context.RoomBookings.AnyAsync(b =>
                b.RoomId == rb.RoomId &&
                b.CheckIn.HasValue && b.CheckOut.HasValue &&
                b.CheckIn.Value < checkOut &&
                b.CheckOut.Value > checkIn);
            if (overlap) return Conflict("Ez a szoba már foglalt a megadott időszakra.");

            rb.Status = "booked";
            rb.CreatedAt = DateTime.UtcNow;
            _context.RoomBookings.Add(rb);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{rb.Id}", rb);
        }

        [Authorize(Policy = "RoomBooking.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, RoomBooking rb)
        {
            if (rb?.UserId == null || rb?.RoomId == null) { return BadRequest("Missing data"); }

            var oldRb =await _context.RoomBookings.FirstOrDefaultAsync(p => p.Id == id);
            if (oldRb == null) return NotFound();
            oldRb.UserId = rb.UserId;
            oldRb.RoomId = rb.RoomId;
            oldRb.CheckIn = rb.CheckIn;
            oldRb.CheckOut = rb.CheckOut;
            oldRb.Rating = rb.Rating;
            await _context.SaveChangesAsync();
            return Ok(oldRb);
        }

        [Authorize(Policy = "RoomBooking.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var rb = await _context.RoomBookings.FirstOrDefaultAsync(p => p.Id == id);
            if (rb == null) return NotFound();
            _context.RoomBookings.Remove(rb);
            await _context.SaveChangesAsync();
            return Ok(rb);
        }
    }
}
