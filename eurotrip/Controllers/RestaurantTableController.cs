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
    public class RestaurantTableController : ControllerBase
    {
        EuroContext _context;

        public RestaurantTableController(EuroContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "RestaurantTable.ReadId")]
        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetTableListByRestaurantId(int id)
        {
            var list = await _context.RestaurantTables.Where(m => m.RestaurantId == id).ToListAsync();
            if (!list.Any()) return NotFound();
            return Ok(list);
        }
        [Authorize(Policy = "RestaurantTable.Create")]
        [HttpPost]
        public async Task<IActionResult> PostTable([FromBody] RestaurantTable rt)
        {
            if(!rt.RestaurantId.HasValue) { return BadRequest("Missing data"); }
            _context.RestaurantTables.Add(rt);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{rt.Id}", rt);
        }
        [Authorize(Policy = "RestaurantTable.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, RestaurantTable rt)
        {
            if (rt.RestaurantId==null) { return BadRequest("Missing data"); }
            var oldRt = await _context.RestaurantTables.FirstOrDefaultAsync(p => p.Id == id);
            if (oldRt == null) return NotFound();
            oldRt.RestaurantId = rt.RestaurantId;
            oldRt.Seats = rt.Seats;
            await _context.SaveChangesAsync();
            return Ok(oldRt);
        }
        [Authorize(Policy = "RestaurantTable.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var rt =await _context.RestaurantTables.FirstOrDefaultAsync(p => p.Id == id);
            if (rt == null) return NotFound();
            _context.RestaurantTables.Remove(rt);
            await _context.SaveChangesAsync();
            return Ok(rt);
        }
    }
}
