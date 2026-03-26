using eurotrip.Modell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eurotrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        EuroContext _context;

        public CityController(EuroContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCityList()
        {
            // Az Include segítségével betöltjük a kapcsolódó ország adatokat is
            var list = await _context.Cities
                .Include(c => c.Country)
                .ToListAsync();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityListById(int id)
        {
            City? city = await _context.Cities.FirstOrDefaultAsync(m => m.Id == id);
            if (city == null) return NotFound();
            return Ok(city);
        }
        [HttpGet("country/{id}")]
        public async Task<IActionResult> GetCityListByCountryId(int id)
        {
            var list = await _context.Cities.Where(m => m.CountryId == id).ToListAsync();
            if (!list.Any()) return NotFound();
            return Ok(list);
        }
    }
}
