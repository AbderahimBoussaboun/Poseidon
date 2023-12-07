using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Servers
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class InfraestructureController : ControllerBase
    {
        private readonly ILogger<InfraestructureController> _logger;
        private readonly IInfraestructuresService _infraestructuresService;

        public InfraestructureController(ILogger<InfraestructureController> logger, IInfraestructuresService infraestructuresService)
        {
            _logger = logger;
            _infraestructuresService = infraestructuresService;
        }

        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Infraestructure>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _infraestructuresService.GetInfraestructures();
            return Ok(result);
        }

        [HttpGet("{infraestructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Infraestructure))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid infraestructureId)
        {
            var result = await _infraestructuresService.GetInfraestructureById(infraestructureId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertProduct([FromBody] InfraestructureRequest infraestructure)
        {
            var result = await _infraestructuresService.InsertInfraestructure(infraestructure);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{infraestructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid infraestructureId, [FromBody] InfraestructureRequest infraestructure)
        {
            var result = await _infraestructuresService.UpdateInfraestructure(infraestructureId, infraestructure);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{infraestructureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid infraestructureId)
        {
            var result = await _infraestructuresService.DeleteInfraestructure(infraestructureId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion

    }
}
