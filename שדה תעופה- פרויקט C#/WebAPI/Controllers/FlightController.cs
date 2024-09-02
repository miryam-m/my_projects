
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL;
using Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightBl flightBl;

        public FlightController(FlightBl _flightBl)
        {
            flightBl = _flightBl;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<List<Flight_DTO>>> GetAllFlights()
        {
            var flights = await flightBl.GetAllFlightsAsync();
            return Ok(flights);
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight_DTO>> GetFlightById(int id)
        {
            var flight = await flightBl.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        // POST: api/Flight
        [HttpPost]
        public async Task<ActionResult> AddFlight([FromBody] Flight_DTO flightDto)
        {
            if (flightDto == null)
            {
                return BadRequest();
            }

            await flightBl.AddFlightAsync(flightDto);
            return CreatedAtAction(nameof(GetFlightById), new { id = flightDto.FlightId }, flightDto);
        }

        // PUT: api/Flight/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlight(int id, [FromBody] Flight_DTO flightDto)
        {
            if (flightDto == null || flightDto.FlightId != id)
            {
                return BadRequest();
            }

            await flightBl.UpdateFlightAsync(flightDto);
            return NoContent();
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            var result = await flightBl.DeleteFlightAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}