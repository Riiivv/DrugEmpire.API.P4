using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugEmpire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _paymentTransactionService;

        public PaymentTransactionController(IPaymentTransactionService paymentTransactionService)
        {
            _paymentTransactionService = paymentTransactionService;
        }

        // GET: api/PaymentTransaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTransactionDTOResponse>>> GetAllPaymentTransactions()
        {
            var items = await _paymentTransactionService.GetAllPaymentTransactions();
            return Ok(items);
        }

        // GET: api/PaymentTransaction/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaymentTransactionDTOResponse>> GetPaymentTransactionById(int id)
        {
            var item = await _paymentTransactionService.GetPaymentTransactionById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/PaymentTransaction
        [HttpPost]
        public async Task<ActionResult<PaymentTransactionDTOResponse>> CreatePaymentTransaction(
            [FromBody] PaymentTransactionDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _paymentTransactionService.CreatePaymentTransaction(request);
            if (created == null)
                return BadRequest("Failed to create payment transaction.");

            return CreatedAtAction(
                nameof(GetPaymentTransactionById),
                new { id = created.PaymentTransactionId },
                created
            );
        }

        // PUT: api/PaymentTransaction/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PaymentTransactionDTOResponse>> UpdatePaymentTransaction(
            int id,
            [FromBody] PaymentTransactionDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis DTO har PaymentTransactionId, så håndhæv match
            if (request.PaymentTransactionId > 0 && id != request.PaymentTransactionId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _paymentTransactionService.UpdatePaymentTransaction(id, request);
            return Ok(updated);
        }

        // DELETE: api/PaymentTransaction/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaymentTransaction(int id)
        {
            var deleted = await _paymentTransactionService.DeletePaymentTransaction(id);
            if (!deleted) return NotFound("Payment transaction not found");

            return NoContent();
        }
    }
}