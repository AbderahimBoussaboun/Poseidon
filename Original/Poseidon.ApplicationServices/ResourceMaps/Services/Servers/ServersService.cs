using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Servers
{
    public class ServersService : IServersService
    {
        private readonly IServersRepository _serversRepository;

        public ServersService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }

        public async Task<bool> DeleteServer(Guid id)
        {
            var result = await _serversRepository.DeleteServerEntity(id);
            return result;
        }

        public async Task<List<Role>> GetAllRolesByServerId(Guid serverId)
        {
            var result = await _serversRepository.GetAllRolesByServerId(serverId);
            return result;
        }

        public async Task<List<ServerApplication>> GetAllServerApplicationsByRoleId(Guid serverId, Guid roleId)
        {
            var result = await _serversRepository.GetAllServerApplicationsByRoleId(serverId, roleId);
            return result;
        }

        public async Task<Role> GetRoleByServerId(Guid serverId, Guid roleId)
        {
            var result = await _serversRepository.GetRoleByServerId(serverId, roleId);
            return result;
        }

        public async Task<ServerApplication> GetServerApplicationByRoleId(Guid serverId, Guid roleId, Guid serverApplicationId)
        {
            var result = await _serversRepository.GetServerApplicationByRoleId(serverId, roleId,serverApplicationId);
            return result;
        }

        public async Task<Server> GetServerById(Guid serverId)
        {
            var result = await _serversRepository.GetServerById(serverId);
            return result;
        }

        public async Task<List<Server>> GetServers()
        {
            var result = await _serversRepository.GetAllServers();
            return result;
        }

        public async Task<Guid> InsertServer(ServerRequest server)
        {
            var temp = new Server() { 
                Name = server.Name,
                Location=server.Location,
                Ip=server.Ip,
                OS=server.OS,
                EnvironmentId=server.EnvironmentId,
                ProductId=server.ProductId
            };
            var result =await _serversRepository.InsertServerEntity(temp);
            return result;
        }

        public async Task<bool> UpdateServer(Guid serverId,ServerRequest server)
        {

            var temp = new Server()
            {
                Id = serverId,
                Name = server.Name,
                Location = server.Location,
                Ip = server.Ip,
                OS = server.OS,
                EnvironmentId = server.EnvironmentId,
                ProductId = server.ProductId,
                Active = server.Active
            };
            var result =await _serversRepository.UpdateServerEntity(temp);
            return result;
        }
    }
}
