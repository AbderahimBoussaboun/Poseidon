using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Servers
{
    public class ServerApplicationsService : IServerApplicationsService
    {
        private readonly IServersRepository _serversRepository;


        public ServerApplicationsService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }

        public async Task<bool> DeleteServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId)
        {
            var result = await _serversRepository.DeleteServerApplicationEntity(serverId, roleId,serverApplicationId);
            return result;
        }

        public async Task<ServerApplication> GetServerApplicationById( Guid serverApplicationId)
        {
            var result = await _serversRepository.GetServerApplicationById(serverApplicationId);
            return result;
        }

        public async Task<List<ServerApplication>> GetAllServerApplications()
        {
            var result = await _serversRepository.GetAllServerApplications();
            return result;
        }

        public async Task<Guid> InsertServerApplication(Guid serverId, Guid roleId,ServerApplicationRequest serverApplication)
        {
            var temp = new ServerApplication() { 
                Name = serverApplication.Name,
                RoleId=serverApplication.RoleId ,
                Active = serverApplication.Active
            };
            var result =await _serversRepository.InsertServerApplicationEntity(serverId, roleId,temp);
            return result;
        }

        public async Task<bool> UpdateServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId,ServerApplicationRequest serverApplication)
        {
            var temp = new ServerApplication() { 
                Id = serverApplication.Id, 
                Name = serverApplication.Name,
                RoleId = serverApplication.RoleId,
                Active = serverApplication.Active
            };

            var result =_serversRepository.UpdateServerApplicationEntity(serverId, roleId, serverApplicationId,temp);
            return await result;
        }
    }
}
