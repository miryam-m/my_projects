
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL;
using Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Booking_BL bookingBl;

        public BookingController(Booking_BL _bookingBl)
        {
            bookingBl = _bookingBl;
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking_DTO bookingDto)
        {
            if (bookingDto == null)
                return BadRequest("Booking data is required.");

            await bookingBl.AddBookingAsync(bookingDto);
            return CreatedAtAction(nameof(GetBooking), new { bookingId = bookingDto.BookingId }, bookingDto);
        }

        // PUT: api/booking
        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] Booking_DTO bookingDto)
        {
            if (bookingDto == null)
                return BadRequest("Booking data is required.");

            await bookingBl.UpdateBookingAsync(bookingDto);
            return NoContent();
        }

        // DELETE: api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await bookingBl.DeleteBookingAsync(id);
            return NoContent();
        }

        // GET: api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await bookingBl.GetBookingAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // GET: api/booking
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await bookingBl.SelectAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/booking/passenger/{passengerId}
        [HttpGet("passenger/{passengerId}")]
        public async Task<IActionResult> GetBookingsByPassengerId(int passengerId)
        {
            var bookings = await bookingBl.GetBookingsByPassengerIdAsync(passengerId);
            return Ok(bookings);
        }
    }
}