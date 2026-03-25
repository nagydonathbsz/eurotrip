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
    public class CountryController : ControllerBase
    {
        EuroContext _context;

        public CountryController(EuroContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCountryList()
        {
            var list = await _context.Countries.ToListAsync();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryListById(int id)
        {
            Country? country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null) return NotFound();
            return Ok(country);
        }
    }
}
