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
    public class AccommodationController : ControllerBase
    {
        EuroContext _context;

        public AccommodationController(EuroContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccommodationList()
        {
            var list = await _context.Accommodations.ToListAsync();
            return Ok(list);
        }
        [Authorize(Policy = "Accommodation.ReadId")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccommodationListById(int id)
        {
            Accommodation? acco = await _context.Accommodations.FirstOrDefaultAsync(m => m.Id == id);
            if (acco == null) return NotFound();
            return Ok(acco);
        }
        [HttpGet("city/{id}")]
        public async Task<IActionResult> GetAccommodationListByCityId(int id)
        {
            var list = await _context.Accommodations.Where(m => m.CityId == id).ToListAsync();
            if (list == null || !list.Any()) return NotFound();

            return Ok(list);
        }
        [Authorize(Policy = "Accommodation.Create")]
        [HttpPost]
        public async Task<IActionResult> PostAccommodation([FromBody]Accommodation acco)
        {
            if (string.IsNullOrWhiteSpace(acco.Name) || 
                string.IsNullOrWhiteSpace(acco.Address)
                ) return BadRequest("Missing datas");
            var m1 = await _context.Accommodations.FirstOrDefaultAsync(m => m.Name == acco.Name);
            if (m1 != null)
            {
                return BadRequest("This accommodation already exist!");
            }

            _context.Accommodations.Add(acco);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{acco.Id}", acco);
        }
        [Authorize(Policy = "Accommodation.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccommodation(int id, Accommodation acco)
        {
            if (string.IsNullOrWhiteSpace(acco.Name) ||
                string.IsNullOrWhiteSpace(acco.Address)
                ) return BadRequest("Missing datas");
            var oldAcco = await _context!.Accommodations.FirstOrDefaultAsync(p => p.Id == id);
            if (oldAcco == null) return NotFound();
            oldAcco.Name = acco.Name;
            oldAcco.Address = acco.Address;
            oldAcco.Image = acco.Image;
            oldAcco.Phone = acco.Phone;
            oldAcco.CityId = acco.CityId;
            await _context!.SaveChangesAsync();
            return Ok(oldAcco);
        }
        [Authorize(Policy = "Accommodation.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var acco =await _context.Accommodations.FirstOrDefaultAsync(p => p.Id == id);
            if (acco == null) return NotFound();
            _context.Accommodations.Remove(acco);
            await _context.SaveChangesAsync();
            return Ok(acco);
        }
    }
}
