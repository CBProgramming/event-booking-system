using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingController : ControllerBase
    {
        private readonly MenusDbContext _context;

        public FoodBookingController(MenusDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservation([FromBody] int eventId)
        {
            var booking = await _context.FoodBookings.FindAsync(eventId);
            if (booking == null)
            {
                return NotFound();
            }
            FoodBookingDto bookingDto= new FoodBookingDto(booking);
            return Ok(bookingDto);
        }
    }
}