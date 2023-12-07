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
    public class ServersController : ControllerBase
    {
        private readonly ILogger<ServersController> _logger;
        private readonly IServersService _serversService;
        private readonly IRolesService _rolesService;
        private readonly IServerApplicationsService _serverApplicationsService;
        public ServersController(ILogger<ServersController> logger, IServersService serversService, IRolesService rolesService, IServerApplicationsService serverApplicationsService)
        {
            _logger = logger;
            _serversService = serversService;
            _rolesService = rolesService;
            _serverApplicationsService = serverApplicationsService;
        }


        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Server>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllServers()
        {
            var result= await _serversService.GetServers();
            return Ok(result);
        }

        [HttpGet("{serverId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Server))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServerById(Guid serverId)
        {
            var result= await _serversService.GetServerById(serverId);
            return Ok(result);
        }


        [HttpGet("{serverId}/Roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Role>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllRoles(Guid serverId)
        {
            var result = await _serversService.GetAllRolesByServerId(serverId);
            return Ok(result);
        }

        [HttpGet("{serverId}/Roles/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Role))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoleById(Guid serverId,Guid roleId)
        {
            var result = await _serversService.GetRoleByServerId(serverId,roleId);
            return Ok(result);
        }

        [HttpGet("{serverId}/Roles/{roleId}/ServerApplications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ServerApplication>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllServerApplications(Guid serverId, Guid roleId)
        {
            var result = await _serversService.GetAllServerApplicationsByRoleId(serverId, roleId);
            return Ok(result);
        }

        [HttpGet("{serverId}/Roles/{roleId}/ServerApplications/{serverApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServerApplication))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServerApplicationById(Guid serverId, Guid roleId,Guid serverApplicationId)
        {
            var result = await _serversService.GetServerApplicationByRoleId(serverId, roleId,serverApplicationId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertServer([FromBody] ServerRequest server)
        {
            var result= await _serversService.InsertServer(server);
            return Ok(result);
        }


        [HttpPost("{serverId}/Roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertRole(Guid serverId,[FromBody] RoleRequest role)
        {
            var result = await _rolesService.InsertRole(serverId,role);
            return Ok(result);
        }

        [HttpPost("{serverId}/Roles/{roleId}/ServerApplications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertServerApplication(Guid serverId, Guid roleId, [FromBody] ServerApplicationRequest serverApplication)
        {
            var result = await _serverApplicationsService.InsertServerApplication(serverId, roleId, serverApplication);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{serverId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateServer(Guid serverId,[FromBody] ServerRequest server)
        {
            var result= await _serversService.UpdateServer(serverId,server);
            return Ok(result);
        }

        [HttpPut("{serverId}/Roles/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRole(Guid serverId, Guid roleId, [FromBody] RoleRequest role)
        {
            var result = await _rolesService.UpdateRole(serverId, roleId,role);
            return Ok(result);
        }

        [HttpPut("{serverId}/Roles/{roleId}/ServerApplications/{serverApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId,[FromBody] ServerApplicationRequest serverApplication)
        {
            var result = await _serverApplicationsService.UpdateServerApplication(serverId, roleId, serverApplicationId,serverApplication);
            return Ok(result);
        }

        #endregion

        #region DELETE
        [HttpDelete("{serverId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteServer(Guid serverId)
        {
            var result= await _serversService.DeleteServer(serverId);
            return Ok(result);
        }

        [HttpDelete("{serverId}/Roles/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(Guid serverId, Guid roleId)
        {
            var result = await _rolesService.DeleteRole(serverId, roleId);
            return Ok(result);
        }

        [HttpDelete("{serverId}/Roles/{roleId}/ServerApplications/{serverApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId)
        {
            var result = await _serverApplicationsService.DeleteServerApplication(serverId, roleId, serverApplicationId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion
    }
}
