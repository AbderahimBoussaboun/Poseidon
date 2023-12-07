using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IRolesService
    {
        public Task<List<Role>> GetAllRoles();
        public Task<Role> GetRoleById(Guid roleId);
        public Task<Guid> InsertRole(Guid serverId, RoleRequest role);
        public Task<bool> DeleteRole(Guid serverId, Guid roleId);
        public Task<bool> UpdateRole(Guid serverId, Guid roleId,RoleRequest role);
    }
}
