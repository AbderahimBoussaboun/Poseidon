using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Applications
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class ComponentTypeController : ControllerBase
    {
        private readonly ILogger<ComponentTypeController> _logger;
        private readonly IComponentTypesService _componentTypesService;

        public ComponentTypeController(ILogger<ComponentTypeController> logger, IComponentTypesService componentTypesService)
        {
            _logger = logger;
            _componentTypesService = componentTypesService;
        }

        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ComponentType>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _componentTypesService.GetComponentTypes();
            return Ok(result);
        }

        [HttpGet("{componentTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ComponentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid componentTypeId)
        {
            var result = await _componentTypesService.GetComponentTypeById(componentTypeId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertProduct([FromBody] ComponentTypeRequest componentType)
        {
            var result = await _componentTypesService.InsertComponentType(componentType);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{componentTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid componentTypeId, [FromBody] ComponentTypeRequest componentType)
        {
            var result = await _componentTypesService.UpdateComponentType(componentTypeId, componentType);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{componentTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid componentTypeId)
        {
            var result = await _componentTypesService.DeleteComponentType(componentTypeId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion


    }
}
