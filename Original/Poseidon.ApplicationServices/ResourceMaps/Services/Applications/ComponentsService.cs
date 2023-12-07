using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Applications
{
    public class ComponentsService : IComponentsService
    {
        private readonly IApplicationsRepository _applicationsRepository;

        public ComponentsService(IApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }
        public async Task<bool> DeleteComponent(Guid applicationId, Guid subApplicationId, Guid componentId)
        {
            var result = await _applicationsRepository.DeleteComponentEntity(applicationId, subApplicationId,componentId);
            return result;
        }

        public async Task<Component> GetComponentById(Guid id)
        {
            var result = await _applicationsRepository.GetComponentById(id);
            return result;
        }

        public async Task<List<Component>> GetComponents()
        {
            var result = await _applicationsRepository.GetAllComponents();
            return result;
        }

        public async Task<Guid> InsertComponent(Guid applicationId, Guid subApplicationId,ComponentRequest component)
        {
            var temp = new Component() {            
                Name = component.Name,
                Ip = component.Ip,
                Ports = component.Ports,
                QueryString=component.QueryString,
                SubApplicationId = subApplicationId,
                BalancerId=component.BalancerId,
                ComponentTypeId=component.ComponentTypeId,
                Active = component.Active
            };
            var result =await _applicationsRepository.InsertComponentEntity(applicationId, subApplicationId,temp);
            return result;
        }

        public async Task<bool> UpdateComponent(Guid applicationId, Guid subApplicationId, Guid componentId, ComponentRequest component)
        {
            var temp = new Component()
            {
                Id = componentId,
                Name = component.Name,
                Ip = component.Ip,
                Ports = component.Ports,
                QueryString = component.QueryString,
                SubApplicationId = component.SubApplicationId,
                BalancerId = component.BalancerId,
                ComponentTypeId = component.ComponentTypeId,
                Active=component.Active
            };
            var result =await _applicationsRepository.UpdateComponentEntity(applicationId, subApplicationId, componentId,temp);
            return result;
        }
    }
}
