
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using Entities;
using DAL;
using DAL.models;
using System.Threading.Tasks;

namespace WebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FlightDetailsController : ControllerBase
        {
            private readonly FlightDetails_BL _flightDetailsBL;

            public FlightDetailsController(FlightDetails_BL flightDetailsBL)
            {
                _flightDetailsBL = flightDetailsBL;
            }

            // GET: api/flightdetails
            [HttpGet]
            public async Task<IActionResult> GetAllFlightDetails()
            {
                var flightDetails = await _flightDetailsBL.GetAllFlightDetailsAsync();
                return Ok(flightDetails);
            }

            // GET: api/flightdetails/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetFlightDetail(int id)
            {
                var flightDetail = await _flightDetailsBL.GetFlightDetailAsync(id);
                if (flightDetail == null)
                {
                    return NotFound();
                }
                return Ok(flightDetail);
            }

            // POST: api/flightdetails
            [HttpPost]
            public async Task<IActionResult> AddFlightDetail([FromBody] FlightDetails_DTO flightDetailDTO)
            {
                if (flightDetailDTO == null)
                {
                    return BadRequest("Flight data required.");
                }
                await _flightDetailsBL.AddFlightDetailAsync(flightDetailDTO);
                return CreatedAtAction(nameof(GetFlightDetail), new { id = flightDetailDTO.FlightDetailsId }, flightDetailDTO);
            }

            // PUT: api/flightdetails/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateFlightDetail(int id, [FromBody] FlightDetails_DTO flightDetailDTO)
            {
                if (flightDetailDTO == null || flightDetailDTO.FlightDetailsId != id)
                {
                    return BadRequest("The flight data is incorrect.");
                }
                await _flightDetailsBL.UpdateFlightDetailAsync(flightDetailDTO);
                return NoContent();
            }

            // DELETE: api/flightdetails/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteFlightDetail(int id)
            {
                await _flightDetailsBL.DeleteFlightDetailAsync(id);
                return NoContent();
            }
        }
    }


