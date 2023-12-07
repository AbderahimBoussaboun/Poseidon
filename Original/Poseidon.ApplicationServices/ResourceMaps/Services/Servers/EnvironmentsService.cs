using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Servers
{
    public class EnvironmentsService : IEnvironmentsService
    {
        private readonly IServersRepository _serversRepository;

        public EnvironmentsService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }

        public async Task<bool> DeleteEnvironment(Guid id)
        {
            var result = await _serversRepository.DeleteEnvironmentEntity(id);
            return result;
        }

        public async Task<Entities.ResourceMaps.Servers.Environment> GetEnvironmentById(Guid id)
        {
            var result = await _serversRepository.GetEnvironmentById(id);
            return result;
        }

        public async Task<List<Entities.ResourceMaps.Servers.Environment>> GetEnvironments()
        {
            var result = await _serversRepository.GetAllEnvironments();
            return result;
        }

        public async Task<Guid> InsertEnvironment(EnvironmentRequest environment)
        {
            var temp = new Entities.ResourceMaps.Servers.Environment() { Name = environment.Name, Active = environment.Active };
            var result =await _serversRepository.InsertEnvironmentEntity(temp);
            return result;
        }

        public async Task<bool> UpdateEnvironment(EnvironmentRequest environment)
        {
            var temp = new Entities.ResourceMaps.Servers.Environment() { Id=environment.Id,Name = environment.Name, Active = environment.Active };
            var result =await _serversRepository.UpdateEnvironmentEntity(temp);
            return result;
        }
    }
}
