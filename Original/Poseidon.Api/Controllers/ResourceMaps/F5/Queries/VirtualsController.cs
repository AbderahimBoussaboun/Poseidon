using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Http;

namespace Poseidon.Api.Controllers.ResourceMaps.F5.Queries
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class VirtualsController : ControllerBase
    {
        private readonly IVirtualsRepository _virtualsRepository;

        public VirtualsController(IVirtualsRepository virtualsRepository)
        {
            _virtualsRepository = virtualsRepository;
        }

        // GET: api/virtual
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Virtual>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Virtual>>> GetVirtuals()
        {
            var virtuals = await _virtualsRepository.GetVirtuals();
            return Ok(virtuals);
        }


        // GET: api/virtual/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Virtual>> GetVirtual(Guid id)
        {
            var virtualEntity = await _virtualsRepository.GetById<Virtual>(id);

            if (virtualEntity == null)
            {
                return NotFound();
            }

            return Ok(virtualEntity);
        }

        // POST: api/virtual
        [HttpPost]
        public async Task<ActionResult<Virtual>> PostVirtual(Virtual virtualEntity)
        {
            await _virtualsRepository.InsertEntity(virtualEntity);
            return CreatedAtAction(nameof(GetVirtual), new { id = virtualEntity.Id }, virtualEntity);
        }

        // PUT: api/virtual/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirtual(int id, Virtual virtualEntity)
        {
            if (id != virtualEntity.Id.GetHashCode())
            {
                return BadRequest();
            }

            var result = await _virtualsRepository.UpdateEntity(virtualEntity);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/virtual/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVirtual(Guid id)
        {
            var result = await _virtualsRepository.DeleteEntity<Virtual>(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
