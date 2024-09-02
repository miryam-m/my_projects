
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
    public class PaymentController : ControllerBase
    {
        private readonly Payment_BL _paymentBL;

        public PaymentController(Payment_BL paymentBL)
        {
            _paymentBL = paymentBL;
        }

        // POST: api/payment
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] Payment_DTO paymentDTO)
        {
            if (paymentDTO == null)
            {
                return BadRequest("Payment data required.");
            }
            await _paymentBL.AddPaymentAsync(paymentDTO);
            return CreatedAtAction(nameof(GetPayment), new { id = paymentDTO.PaymentId }, paymentDTO);
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment_DTO paymentDTO)
        {
            if (paymentDTO == null || paymentDTO.PaymentId != id)
            {
                return BadRequest("The payment data is incorrect.");
            }
            await _paymentBL.UpdatePaymentAsync(paymentDTO);
            return NoContent();
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _paymentBL.DeletePaymentAsync(id);
            return NoContent();
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var paymentDTO = await _paymentBL.GetPaymentAsync(id);
            if (paymentDTO == null)
            {
                return NotFound();
            }
            return Ok(paymentDTO);
        }

        // GET: api/payment
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentBL.GetAllPaymentsAsync();
            return Ok(payments);
        }

        // GET: api/payment/owner/{ownerId}
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetPaymentsByOwner(string ownerId)
        {
            var payments = await _paymentBL.GetPaymentsByOwnerIdAsync(ownerId);
            return Ok(payments);
        }
    }
}