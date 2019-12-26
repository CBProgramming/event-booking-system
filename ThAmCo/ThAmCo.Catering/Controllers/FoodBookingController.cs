using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Controllers
{
    //Resful catering API used for managing menu bookings
    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingController : ControllerBase
    {
        private readonly MenusDbContext _context;

        public FoodBookingController(MenusDbContext context)
        {
            _context = context;
        }

        //Get bookingDto based on provided event id
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

        //Edit existing menu booking using provided bookingDto containing event id and menu id
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

        //Create new menu booking using provided bookingDto containing event id and menu id
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

        //Cancel existing food booking based on provided eventId
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