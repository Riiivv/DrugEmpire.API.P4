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
using System.Collections.Generic;

namespace DrugEmpire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTOResponse>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddresses();
            return Ok(addresses);
        }

        // GET: api/Address/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressDTOResponse>> GetAddressById(int id)
        {
            try
            {
                var address = await _addressService.GetAddressById(id);
                if (address == null) return NotFound();
                return Ok(address);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/Address
        [HttpPost]
        public async Task<ActionResult<AddressDTOResponse>> CreateAddress([FromBody] AddressDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var created = await _addressService.CreateAddress(request);
                if (created == null)
                    return BadRequest("Failed to create address.");

                return CreatedAtAction(nameof(GetAddressById), new { id = created.AddressId }, created);
            }
            catch (System.ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Address/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AddressDTOResponse>> UpdateAddress(int id, [FromBody] AddressDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har AddressId, så håndhæv match (som i dit Hall-eksempel)
            if (request.AddressId > 0 && id != request.AddressId)
                return BadRequest("Route ID does not match request body ID.");

            try
            {
                var updated = await _addressService.UpdateAddress(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Address not found.");
            }
            catch (System.ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Address/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var deleted = await _addressService.DeleteAddress(id);
                if (!deleted) return NotFound("Address not found");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Address not found");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}