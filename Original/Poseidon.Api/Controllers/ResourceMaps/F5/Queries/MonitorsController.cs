using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Monitors;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Http;

namespace Poseidon.Api.Controllers.ResourceMaps.F5.Queries
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class MonitorsController : ControllerBase
    {
        private readonly IMonitorsRepository _monitorsRepository;

        public MonitorsController(IMonitorsRepository monitorsRepository)
        {
            _monitorsRepository = monitorsRepository;
        }

        // GET: api/monitors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Monitor>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Monitor>>> GetMonitors()
        {
            var monitors = await _monitorsRepository.GetAllMonitors();
            return Ok(monitors);
        }


        // GET: api/monitors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Monitor>> GetMonitor(Guid id)
        {
            var monitorEntity = await _monitorsRepository.GetById<Monitor>(id);

            if (monitorEntity == null)
            {
                return NotFound();
            }

            return Ok(monitorEntity);
        }

        // POST: api/monitors
        [HttpPost]
        public async Task<ActionResult<Monitor>> PostMonitor(Monitor monitorEntity)
        {
            await _monitorsRepository.InsertEntity(monitorEntity);
            return CreatedAtAction(nameof(GetMonitor), new { id = monitorEntity.Id }, monitorEntity);
        }

        // PUT: api/monitors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonitor(Guid id, Monitor monitorEntity)
        {
            if (id != monitorEntity.Id)
            {
                return BadRequest();
            }

            var result = await _monitorsRepository.UpdateEntity(monitorEntity);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/monitors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitor(Guid id)
        {
            var result = await _monitorsRepository.DeleteEntity<Monitor>(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
