using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Balancers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Balancers;
using Poseidon.Entities.ResourceMaps.Balancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Balancers
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class BalancersController : ControllerBase
    {
        private readonly ILogger<BalancersController> _logger;
        private readonly IBalancersService _BalancersService;

        public BalancersController(ILogger<BalancersController> logger, IBalancersService balancersService)
        {
            _logger = logger;
            _BalancersService = balancersService;
        }


        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Balancer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBalancers()
        {
            var result= await _BalancersService.GetBalancers();
            return Ok(result);
        }

        [HttpGet("{balancerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Balancer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBalancerById(Guid balancerId)
        {
            var result= await _BalancersService.GetBalancerById(balancerId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertBalancer([FromBody] BalancerRequest balancer)
        {
            var result= await _BalancersService.InsertBalancer(balancer);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{balancerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBalancer(Guid balancerId,[FromBody] BalancerRequest balancer)
        {
            var result= await _BalancersService.UpdateBalancer(balancerId,balancer);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{balancerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBalancer(Guid balancerId)
        {
            var result= await _BalancersService.DeleteBalancer(balancerId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion
    }
}
