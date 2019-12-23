using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetReservation(int eventId)
        {
            var booking = await _context.FoodBookings.FindAsync(eventId);
            if (booking == null)
            {
                return NotFound();
            }
            FoodBookingDto bookingDto= new FoodBookingDto(booking);
            return Ok(bookingDto);
        }

        [HttpPut]
        public async Task<IActionResult> EditReservation([FromBody] FoodBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var booking = await _context.FoodBookings.FindAsync(bookingDto.EventId);
            if (booking != null)
            {
                try
                {
                    booking.MenuNumber = bookingDto.MenuId;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {

                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] FoodBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var booking = new FoodBooking(bookingDto);
            try
            {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return Ok();
            }
            catch (Exception e)
            {

            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(int eventId)
        {
            var booking = await _context.FoodBookings.FindAsync(eventId);
            if (booking != null)
            {
                try
                {
                    _context.FoodBookings.Remove(booking);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {

                }
            }
            return NotFound();
        }
    }
}