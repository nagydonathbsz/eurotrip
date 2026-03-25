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
    public class RestaurantController : ControllerBase
    {
        EuroContext _context;

        public RestaurantController(EuroContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetRestaurantList()
        {
            var list = await _context.Restaurants.ToListAsync();
            return Ok(list);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantListById(int id)
        {
            Restaurant? restaurant = await _context.Restaurants.FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }
        [AllowAnonymous] 
        [HttpGet("city/{id}")]
        public async Task<IActionResult> GetRestaurantListByCityId(int id)
        {
            var restaurants = await _context.Restaurants
                .Where(m => m.CityId == id)
                .ToListAsync();

            return Ok(restaurants);
        }
        [Authorize(Policy = "Restaurant.Create")]
        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromBody] Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Name) ||
                string.IsNullOrWhiteSpace(restaurant.Address) ||
                string.IsNullOrWhiteSpace(restaurant.Phone)) return BadRequest("Missing datas");
            var m1 = await _context.Restaurants.FirstOrDefaultAsync(m => m.Name == restaurant.Name);
            if (m1 != null)
            {
                return BadRequest("This restaurant already exist!");
            }

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{restaurant.Id}", restaurant);
        }
        [Authorize(Policy = "Restaurant.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Name) ||
                string.IsNullOrWhiteSpace(restaurant.Address)
                ) return BadRequest("Missing datas");
            var oldRestaurant =await _context.Restaurants.FirstOrDefaultAsync(p => p.Id == id);
            if (oldRestaurant == null) return NotFound();
            oldRestaurant.Name = restaurant.Name;
            oldRestaurant.Address = restaurant.Address;
            oldRestaurant.Image = restaurant.Image;
            oldRestaurant.Phone = restaurant.Phone;
            oldRestaurant.CityId = restaurant.CityId;
            await _context.SaveChangesAsync();
            return Ok(oldRestaurant);
        }
        [Authorize(Policy = "Restaurant.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant =await _context.Restaurants.FirstOrDefaultAsync(p => p.Id == id);
            if (restaurant == null) return NotFound();
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return Ok(restaurant);
        }
    }
}
