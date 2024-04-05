using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Servers
{
    public interface IServersRepository
    {
        #region SERVER
        public Task<List<Server>> GetAllServers();
        public Task<Server> GetServerById(Guid serverId);
        public Task<List<Role>> GetAllRolesByServerId(Guid serverId);
        public Task<Role> GetRoleByServerId(Guid serverId, Guid roleId);
        public Task<List<ServerApplication>> GetAllServerApplicationsByRoleId(Guid serverId, Guid roleId);
        public Task<ServerApplication> GetServerApplicationByRoleId(Guid serverId, Guid roleId, Guid serverApplicationId);

        public Task<Guid> InsertServerEntity(Server server);
        public Task<bool> UpdateServerEntity(Server server);
        public Task<bool> DeleteServerEntity(Guid serverId);
        #endregion

        #region ROLE
        public Task<List<Role>> GetAllRoles();
        public Task<Role> GetRoleById(Guid roleId);
        public Task<Guid> InsertRoleEntity(Guid serverId, Role role);
        public Task<bool> UpdateRoleEntity(Guid serverId, Guid roleId, Role role);
        public Task<bool> DeleteRoleEntity(Guid serverId, Guid roleId);
        #endregion

        #region SERVERAPPLICATION
        public Task<List<ServerApplication>> GetAllServerApplications();
        public Task<ServerApplication> GetServerApplicationById(Guid serverApplicationId);
        public Task<Guid> InsertServerApplicationEntity(Guid serverId, Guid roleId, ServerApplication serverApplication);
        public Task<bool> UpdateServerApplicationEntity(Guid serverId, Guid roleId, Guid serverApplicationId, ServerApplication serverApplication);
        public Task<bool> DeleteServerApplicationEntity(Guid serverId, Guid roleId, Guid serverApplicationId);
        #endregion

        #region ENVIRONMENT
        public Task<List<Entities.ResourceMaps.Servers.Environment>> GetAllEnvironments();
        public Task<Entities.ResourceMaps.Servers.Environment> GetEnvironmentById(Guid environmentId);
        public Task<Guid> InsertEnvironmentEntity(Entities.ResourceMaps.Servers.Environment environment);
        public Task<bool> UpdateEnvironmentEntity(Entities.ResourceMaps.Servers.Environment environment);
        public Task<bool> DeleteEnvironmentEntity(Guid environmentId);
        #endregion

        #region INFRASTRUCTURE
        public Task<List<Infrastructure>> GetAllInfrastructures();
        public Task<Infrastructure> GetInfrastructureById(Guid infrastructureId);
        public Task<Guid> InsertInfrastructureEntity(Infrastructure infrastructure);
        public Task<bool> UpdateInfrastructureEntity(Infrastructure infrastructure);
        public Task<bool> DeleteInfrastructureEntity(Guid infrastructureId);
        #endregion

    }
}
