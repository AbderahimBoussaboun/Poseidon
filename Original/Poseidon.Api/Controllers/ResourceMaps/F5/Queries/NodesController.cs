using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Nodes;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Http;

namespace Poseidon.Api.Controllers.ResourceMaps.F5.Queries
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class NodesController : ControllerBase
    {
        private readonly INodesRepository _nodesRepository;

        public NodesController(INodesRepository nodesRepository)
        {
            _nodesRepository = nodesRepository;
        }

        // GET: api/node
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Node>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Node>>> GetNodes()
        {
            var nodes = await _nodesRepository.GetAllNodes();
            return Ok(nodes);
        }


        // GET: api/node/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> GetNode(Guid id)
        {
            var nodeEntity = await _nodesRepository.GetById<Node>(id);

            if (nodeEntity == null)
            {
                return NotFound();
            }

            return Ok(nodeEntity);
        }

        // POST: api/node
        [HttpPost]
        public async Task<ActionResult<Node>> PostNode(Node nodeEntity)
        {
            await _nodesRepository.InsertEntity(nodeEntity);
            return CreatedAtAction(nameof(GetNode), new { id = nodeEntity.Id }, nodeEntity);
        }

        // PUT: api/node/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNode(int id, Node nodeEntity)
        {
            if (id != nodeEntity.Id.GetHashCode())
            {
                return BadRequest();
            }

            var result = await _nodesRepository.UpdateEntity(nodeEntity);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/node/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            var result = await _nodesRepository.DeleteEntity<Node>(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
