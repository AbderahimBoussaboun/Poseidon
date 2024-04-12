using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Pools;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Http;

namespace Poseidon.Api.Controllers.ResourceMaps.F5.Queries
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class PoolsController : ControllerBase
    {
        private readonly IPoolsRepository _poolsRepository;

        public PoolsController(IPoolsRepository poolsRepository)
        {
            _poolsRepository = poolsRepository;
        }

        // GET: api/pool
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Pool>>> GetNodes()
        {
            var pools = await _poolsRepository.GetAllPools();
            return Ok(pools);
        }


        // GET: api/pool/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pool>> GetPool(Guid id)
        {
            var poolEntity = await _poolsRepository.GetById<Pool>(id);

            if (poolEntity == null)
            {
                return NotFound();
            }

            return Ok(poolEntity);
        }

        // POST: api/pool
        [HttpPost]
        public async Task<ActionResult<Pool>> PostPool(Pool poolEntity)
        {
            await _poolsRepository.InsertEntity(poolEntity);
            return CreatedAtAction(nameof(GetPool), new { id = poolEntity.Id }, poolEntity);
        }

        // PUT: api/pool/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPool(int id, Pool poolEntity)
        {
            if (id != poolEntity.Id.GetHashCode())
            {
                return BadRequest();
            }

            var result = await _poolsRepository.UpdateEntity(poolEntity);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/pool/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool(Guid id)
        {
            var result = await _poolsRepository.DeleteEntity<Pool>(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
