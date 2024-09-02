
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using BL;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailsController : ControllerBase
    {
        private readonly BookingDetails_BL bookingDetailsBl;

        public BookingDetailsController(BookingDetails_BL _bookingDetailsBl)
        {
            bookingDetailsBl = _bookingDetailsBl;
        }

        // POST: api/BookingDetails
        [HttpPost]
        public async Task<IActionResult> AddBookingDetail([FromBody] BookingDetails_DTO bookingDetailDto)
        {
            if (bookingDetailDto == null)
            {
                return BadRequest("Invalid booking detail.");
            }

            await bookingDetailsBl.AddBookingDetailAsync(bookingDetailDto);
            return CreatedAtAction(nameof(GetBookingDetail), new { id = bookingDetailDto.BookingDetailsId }, bookingDetailDto);
        }

        // PUT: api/BookingDetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingDetail(int id, [FromBody] BookingDetails_DTO bookingDetailDto)
        {
            if (id != bookingDetailDto.BookingDetailsId)
            {
                return BadRequest("Booking detail ID mismatch.");
            }

            await bookingDetailsBl.UpdateBookingDetailAsync(bookingDetailDto);
            return NoContent();
        }

        // DELETE: api/BookingDetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            await bookingDetailsBl.DeleteBookingDetailAsync(id);
            return NoContent();
        }

        // GET: api/BookingDetails/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingDetail(int id)
        {
            var bookingDetail = await bookingDetailsBl.GetBookingDetailAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            return Ok(bookingDetail);
        }

        // GET: api/BookingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDetails_DTO>>> GetAllBookingDetails()
        {
            var bookingDetails = await bookingDetailsBl.SelectAllAsync();
            return Ok(bookingDetails);
        }

        // GET: api/BookingDetails/ByBookingId/{bookingId}
        [HttpGet("ByBookingId/{bookingId}")]
        public async Task<ActionResult<IEnumerable<BookingDetails_DTO>>> GetBookingDetailsByBookingId(int bookingId)
        {
            var bookingDetails = await bookingDetailsBl.GetBookingDetailsByBookingIdAsync(bookingId);
            return Ok(bookingDetails);
        }
    }
}