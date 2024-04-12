using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Rules;
using System.Linq.Expressions;
using System;
using Microsoft.AspNetCore.Http;

namespace Poseidon.Api.Controllers.ResourceMaps.F5.Queries
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly IRulesRepository _rulesRepository;

        public RulesController(IRulesRepository rulesRepository)
        {
            _rulesRepository = rulesRepository;
        }

        // GET: api/rules
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Rule>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Rule>>> GetRules()
        {
            var rules = await _rulesRepository.GetAllRules();
            return Ok(rules);
        }


        // GET: api/rules/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Rule>> GetRule(Guid id)
        {
            var ruleEntity = await _rulesRepository.GetById<Rule>(id);

            if (ruleEntity == null)
            {
                return NotFound();
            }

            return Ok(ruleEntity);
        }

        // POST: api/rules
        [HttpPost]
        public async Task<ActionResult<Rule>> PostRule(Rule ruleEntity)
        {
            await _rulesRepository.InsertEntity(ruleEntity);
            return CreatedAtAction(nameof(GetRule), new { id = ruleEntity.Id }, ruleEntity);
        }

        // PUT: api/rules/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(Guid id, Rule ruleEntity)
        {
            if (id != ruleEntity.Id)
            {
                return BadRequest();
            }

            var result = await _rulesRepository.UpdateEntity(ruleEntity);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/rules/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(Guid id)
        {
            var result = await _rulesRepository.DeleteEntity<Rule>(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
