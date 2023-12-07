using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Servers
{
    public class RolesService : IRolesService
    {
        private readonly IServersRepository _serversRepository;

        public RolesService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }

        public async Task<bool> DeleteRole(Guid serverId, Guid roleId)
        {
            var result = await _serversRepository.DeleteRoleEntity(serverId, roleId);
            return result;
        }

        public async Task<Role> GetRoleById(Guid roleId)
        {
            var result = await _serversRepository.GetRoleById(roleId);
            return result;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var result = await _serversRepository.GetAllRoles();
            return result;
        }

        public async Task<Guid> InsertRole(Guid serverId, RoleRequest role)
        {
            var temp = new Role() {
                Id = role.Id,
                Name = role.Name,
                Type = role.Type,
                ServerId = role.ServerId,
                Active = role.Active
            };
            var result =await _serversRepository.InsertRoleEntity(serverId,temp);
            return result;
        }

        public async Task<bool> UpdateRole(Guid serverId, Guid roleId, RoleRequest role)
        {
            var temp = new Role() { 
                Id = role.Id, 
                Name = role.Name,
                Type=role.Type,
                ServerId=role.ServerId,
                Active=role.Active
            };
            var result =await _serversRepository.UpdateRoleEntity(serverId, roleId,temp);
            return result;
        }
    }
}
