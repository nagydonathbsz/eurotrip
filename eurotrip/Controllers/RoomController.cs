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
    public class RoomController : ControllerBase
    {
        EuroContext _context;

        public RoomController(EuroContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "Room.Read")]
        [HttpGet]
        public async Task<IActionResult> GetRoomList()
        {
            var list = await _context.Rooms.ToListAsync();
            return Ok(list);
        }

        [Authorize(Policy = "Room.ReadId")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomListById(int id)
        {
            Room? room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null) return NotFound();
            return Ok(room);
        }
        [Authorize(Policy = "Room.ReadAccoId")]
        [HttpGet("acco/{accoId}")]
        public async Task<IActionResult> GetRoomListByAccoId(int accoId)
        {
            bool exists = await _context.Accommodations.AnyAsync(m => m.Id == accoId);
            if (!exists) return NotFound();
            var rooms = await _context.Rooms.Where(r => r.AccommodationId == accoId).ToListAsync();
            return Ok(rooms);
        }

        [Authorize(Policy = "Room.Create")]
        [HttpPost]
        public async Task<IActionResult> PostRoom([FromBody] Room room)
        {
            if (room?.AccommodationId == null) { return BadRequest("Missing data"); }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{room.Id}", room);
        }

        [Authorize(Policy = "Room.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (room?.AccommodationId == null) { return BadRequest("Missing data"); }

            var oldRoom = await _context.Rooms.FirstOrDefaultAsync(p => p.Id == id);
            if (oldRoom == null) return NotFound();
            oldRoom.AccommodationId = room.AccommodationId;
            await _context.SaveChangesAsync();
            return Ok(oldRoom);
        }

        [Authorize(Policy = "Room.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room =await _context.Rooms.FirstOrDefaultAsync(p => p.Id == id);
            if (room == null) return NotFound();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return Ok(room);
        }
    }
}
