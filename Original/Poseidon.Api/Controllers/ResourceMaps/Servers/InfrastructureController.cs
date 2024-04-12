using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Services.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Servers
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class InfrastructureController : ControllerBase
    {
        private readonly ILogger<InfrastructureController> _logger;
        private readonly IInfrastructuresService _infrastructuresService;

        public InfrastructureController(ILogger<InfrastructureController> logger, IInfrastructuresService infrastructuresService)
        {
            _logger = logger;
            _infrastructuresService = infrastructuresService;
        }

        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Infrastructure>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _infrastructuresService.GetInfrastructures();
            return Ok(result);
        }

        [HttpGet("{infrastructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Infrastructure))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid infrastructureId)
        {
            var result = await _infrastructuresService.GetInfrastructureById(infrastructureId);
            return Ok(result);
        }
        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertProduct([FromBody] InfrastructureRequest infrastructure)
        {
            var result = await _infrastructuresService.InsertInfrastructure(infrastructure);
            return Ok(result);
        }
        #endregion

        #region PUT
        [HttpPut("{infrastructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid infrastructureId, [FromBody] InfrastructureRequest infrastructure)
        {
            var result = await _infrastructuresService.UpdateInfrastructure(infrastructureId, infrastructure);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{infrastructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid infrastructureId)
        {
            var result = await _infrastructuresService.DeleteInfrastructure(infrastructureId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion
    }
}
