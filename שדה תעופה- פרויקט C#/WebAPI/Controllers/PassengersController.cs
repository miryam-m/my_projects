
using Microsoft.AspNetCore.Mvc;
using BL;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly Passengers_BL _passengersBL;

        public PassengersController(Passengers_BL passengersBL)
        {
            _passengersBL = passengersBL;
        }

        // GET: api/passengers
        [HttpGet]
        public async Task<IActionResult> GetAllPassengers()
        {
            var passengers = await _passengersBL.GetAllPassengersAsync();
            return Ok(passengers);
        }

        // GET: api/passengers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassenger(int id)
        {
            var passenger = await _passengersBL.GetPassengerAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }

       //public async Task<IActionResult> AddPassenger([FromBody] Passengers_DTO passengerDTO)
       // {
       //     if (passengerDTO == null)
       //     {
       //         return BadRequest("Passenger data required.");
       //     }

       //     // המרת DTO לאובייקט Entity
       //     var passengerEntity = new PassengersTbl
       //     {
       //         // העתקת שדות מ-passengerDTO ל-passengerEntity
       //         // לדוגמה:
       //         // Name = passengerDTO.Name,
       //         // Age = passengerDTO.Age,
       //         // וכו'
       //     };

       //     var createdPassenger = await _passengersBL.AddPassengerAsync(passengerEntity);

       //     return CreatedAtAction(nameof(GetPassenger), new { id = createdPassenger.PassengersId }, createdPassenger);
       // }

        // PUT: api/passengers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, [FromBody] Passengers_DTO passengerDTO)
        {
            if (passengerDTO == null || passengerDTO.PassengersId != id)
            {
                return BadRequest("The payment data is incorrect.");
            }
            await _passengersBL.UpdatePassengerAsync(passengerDTO);
            return NoContent();
        }

        // DELETE: api/passengers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            await _passengersBL.DeletePassengerAsync(id);
            return NoContent();
        }
    }
}