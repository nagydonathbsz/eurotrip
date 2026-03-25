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
    public class TableReservationController : ControllerBase
    {
        EuroContext _context;

        public TableReservationController(EuroContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "TableReservation.Read")]
        [HttpGet]
        public async Task<IActionResult> GetTableReservationList()
        {
            var list = await _context.TableReservations.ToListAsync();
            return Ok(list);
        }
        [Authorize(Policy = "TableReservation.ReadMe")]
        [HttpGet]
        public async Task<IActionResult> GetMyTableReservationList()
        {
            var me = await _context.Users.FirstOrDefaultAsync(m => m.Token != null);
            var list = me?.TRs?.Where(m => m.UserId == me.Id).ToList();
            return Ok(list);
        }
        [Authorize(Policy = "TableReservation.ReadIdAvailable")]
        [HttpGet("{restId}")]
        public async Task<IActionResult> GetTableReservationById_IsAvailable(int id)
        {
            var rt = await _context.TableReservations.FirstOrDefaultAsync(m => m.Id == id);
            if (rt == null) return NotFound();
            return Ok(rt.Status?.ToString());
        }

        [Authorize(Policy = "TableReservation.Create")]
        [HttpPost]
        public async Task<IActionResult> PostTableReservation([FromBody] TableReservation tr)
        {
            _context.TableReservations.Add(tr);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{tr.Id}", tr);
        }

        [Authorize(Policy = "TableReservation.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableReservation(int id)
        {
            var tr = await _context.TableReservations.FirstOrDefaultAsync(p => p.Id == id);
            if (tr == null) return NotFound();
            _context.TableReservations.Remove(tr);
            await _context.SaveChangesAsync();
            return Ok(tr);
        }
    }
}
