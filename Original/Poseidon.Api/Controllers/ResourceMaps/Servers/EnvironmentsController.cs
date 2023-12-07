using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Servers
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class EnvironmentsController : ControllerBase
    {

        private readonly ILogger<EnvironmentsController> _logger;
        private readonly IEnvironmentsService _environmentsService;

        public EnvironmentsController(ILogger<EnvironmentsController> logger, IEnvironmentsService environmentsService)
        {
            _logger = logger;
            _environmentsService = environmentsService;
        }


        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Entities.ResourceMaps.Servers.Environment>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEnvironments()
        {
            var result= await _environmentsService.GetEnvironments();
            return Ok(result);
        }

        [HttpGet("{environmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Entities.ResourceMaps.Servers.Environment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEnvironmentById(Guid environmentId)
        {
            var result= await _environmentsService.GetEnvironmentById(environmentId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertEnvironment(EnvironmentRequest environment)
        {
            var result= await _environmentsService.InsertEnvironment(environment);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{environmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEnvironment(Guid environmentId,[FromBody] EnvironmentRequest environment)
        {
            environment.Id = environmentId;
            var result= await _environmentsService.UpdateEnvironment(environment);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{environmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEnvironment(Guid environmentId)
        {
            var result= await _environmentsService.DeleteEnvironment(environmentId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion


    }
}
