using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Entities.ResourceMaps.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Servers
{
    public class ServersRepository : GenericRepository, IServersRepository
    {

        public ServersRepository(DataContext context) : base(context) { }

        #region EXPRESSION_INCLUDES
        Expression<Func<Server, object>>[] includesServer = new Expression<Func<Server, object>>[]
        {
                e => e.Roles
        };

        Expression<Func<Role, object>>[] includesRole = new Expression<Func<Role, object>>[]
        {
                e => e.ServerApplications
        };

        Expression<Func<ServerApplication, object>>[] includesServerApplication = new Expression<Func<ServerApplication, object>>[]
        {
        };

        Expression<Func<Entities.ResourceMaps.Servers.Environment, object>>[] includesEnvironment = new Expression<Func<Entities.ResourceMaps.Servers.Environment, object>>[]
       {
       };

        Expression<Func<Infrastructure, object>>[] includesInfrastructure = new Expression<Func<Infrastructure, object>>[]
       {
       };

        #endregion



        #region SERVER

        public async Task<List<Server>> GetAllServers()
        {
            var result = await base.GetAll(includesServer);
            return result;
        }

        public async Task<Server> GetServerById(Guid id)
        {
            var result = await base.GetById(id, includesServer);
            return result;
        }

        public async Task<List<Role>> GetAllRolesByServerId(Guid serverId)
        {
            try
            {
                var result = await base.GetById(serverId, includesServer);
                return result.Roles.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<Role> GetRoleByServerId(Guid serverId, Guid roleId)
        {
            try
            {
                var result = await base.GetById(serverId, includesServer);
                var role = result.Roles.SingleOrDefault(s => s.Id.Equals(roleId));
                return role;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<List<ServerApplication>> GetAllServerApplicationsByRoleId(Guid serverId, Guid roleId)
        {
            try
            {
                var role = await base.GetById(roleId, includesRole);
                var result = role.ServerId.Equals(serverId) ? role.ServerApplications.ToList() : new List<ServerApplication>();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ServerApplication> GetServerApplicationByRoleId(Guid serverId, Guid roleId, Guid serverApplicationId)
        {
            try
            {
                var server = await base.GetById(serverId, includesServer);
                var roleLocal = server.Roles.SingleOrDefault(s => s.Id == roleId);
                var role = await base.GetById(roleLocal.Id, includesRole);
                var serverApplicationLocal = role.ServerApplications.SingleOrDefault(s => s.Id == serverApplicationId);
                var serverApplication = await base.GetById(serverApplicationLocal.Id, includesServerApplication);
                return serverApplication;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public async Task<Guid> InsertServerEntity(Server server)
        {
            var result = await base.InsertEntity(server);
            return result;
        }

        public async Task<bool> UpdateServerEntity(Server server)
        {
            var result = await base.UpdateEntity(server);
            return result;
        }


        public async Task<bool> DeleteServerEntity(Guid serverId)
        {
            var result = await base.DeleteEntity<Server>(serverId);
            return result;
        }
        #endregion

        #region ROLE
        public async Task<List<Role>> GetAllRoles()
        {
            var result = await base.GetAll(includesRole);
            return result;
        }

        public async Task<Role> GetRoleById(Guid roleId)
        {
            var result = await base.GetById(roleId, includesRole);
            return result;
        }

        public async Task<Guid> InsertRoleEntity(Guid serverId, Role role)
        {
            var server = await base.GetById(serverId, includesServer);
            if (server == null) return Guid.Empty;

            var result = await base.InsertEntity(role);
            return result;
        }

        public async Task<bool> UpdateRoleEntity(Guid serverId, Guid roleId, Role role)
        {
            try
            {
                var server = await base.GetById(serverId, includesServer);
                var roleLocal = server.Roles.SingleOrDefault(s => s.Id == roleId);
                if (roleLocal == null) return false;

                var result = await base.UpdateEntity(role);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteRoleEntity(Guid serverId, Guid roleId)
        {
            try
            {
                var server = await base.GetById(serverId, includesServer);
                var roleLocal = server.Roles.SingleOrDefault(s => s.Id == roleId);
                if (roleLocal == null) return false;

                var result = await base.DeleteEntity<SubApplication>(roleId);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region SERVERAPPLICATION


        public async Task<List<ServerApplication>> GetAllServerApplications()
        {
            var result = await base.GetAll(includesServerApplication);
            return result;
        }

        public async Task<ServerApplication> GetServerApplicationById(Guid serverApplicationId)
        {
            var result = await base.GetById(serverApplicationId, includesServerApplication);
            return result;
        }
        public async Task<Guid> InsertServerApplicationEntity(Guid serverId, Guid roleId, ServerApplication serverApplication)
        {
            try
            {
                var server = await base.GetById(serverId, includesServer);
                var role = server.Roles.SingleOrDefault(s => s.Id.Equals(roleId));
                if (role == null) return Guid.Empty;
                var result = await base.InsertEntity(serverApplication);
                return result;
            }
            catch (Exception)
            {

                return Guid.Empty;
            }

        }
        public async Task<bool> UpdateServerApplicationEntity(Guid serverId, Guid roleId, Guid serverApplicationId, ServerApplication serverApplication)
        {

            try
            {
                var server = await base.GetById(serverId, includesServer);
                var role = server.Roles.SingleOrDefault(s => s.Id.Equals(roleId));
                var serverApplicationLocal = role.ServerApplications.SingleOrDefault(s => s.Id.Equals(serverApplicationId));
                if (serverApplicationLocal == null) return false;
                var result = await base.UpdateEntity(serverApplication);
                return result;
            }
            catch (Exception)
            {
                return false;
            }


        }
        public async Task<bool> DeleteServerApplicationEntity(Guid serverId, Guid roleId, Guid serverApplicationId)
        {
            try
            {
                var server = await base.GetById(serverId, includesServer);
                var role = server.Roles.SingleOrDefault(s => s.Id.Equals(roleId));
                var serverApplicationLocal = role.ServerApplications.SingleOrDefault(s => s.Id.Equals(serverApplicationId));
                if (serverApplicationLocal == null) return false;
                var result = await base.DeleteEntity<ServerApplication>(serverApplicationId);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region ENVIRONMENT
        public async Task<List<Entities.ResourceMaps.Servers.Environment>> GetAllEnvironments()
        {
            var result = await base.GetAll(includesEnvironment);
            return result;
        }

        public async Task<Entities.ResourceMaps.Servers.Environment> GetEnvironmentById(Guid environmentId)
        {
            var result = await base.GetById(environmentId, includesEnvironment);
            return result;
        }

        public async Task<Guid> InsertEnvironmentEntity(Entities.ResourceMaps.Servers.Environment environment)
        {
            var result = await base.InsertEntity(environment);
            return result;
        }

        public async Task<bool> UpdateEnvironmentEntity(Entities.ResourceMaps.Servers.Environment environment)
        {
            var result = await base.UpdateEntity(environment);
            return result;
        }

        public async Task<bool> DeleteEnvironmentEntity(Guid environmentId)
        {
            var result = await base.DeleteEntity<Entities.ResourceMaps.Servers.Environment>(environmentId);
            return result;
        }
        #endregion

        #region INFRASTRUCTURE
        public async Task<List<Infrastructure>> GetAllInfrastructures()
        {
            var result = await base.GetAll(includesInfrastructure);
            return result;
        }
        public async Task<Infrastructure> GetInfrastructureById(Guid infrastructureId)
        {
            var result = await base.GetById(infrastructureId, includesInfrastructure);
            return result;
        }
        public async Task<Guid> InsertInfrastructureEntity(Infrastructure infrastructure)
        {
            var result = await base.InsertEntity(infrastructure);
            return result;
        }
        public async Task<bool> UpdateInfrastructureEntity(Infrastructure infrastructure)
        {
            var result = await base.UpdateEntity(infrastructure);
            return result;
        }
        public async Task<bool> DeleteInfrastructureEntity(Guid infrastructureId)
        {
            var result = await base.DeleteEntity<Infrastructure>(infrastructureId);
            return result;
        }
        #endregion

    }
}
