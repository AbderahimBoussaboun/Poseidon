using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Applications
{
    public class ComponentTypesService : IComponentTypesService
    {

        private readonly IApplicationsRepository _applicationsRepository;

        public ComponentTypesService(IApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }


        public async Task<bool> DeleteComponentType(Guid componentTypeId)
        {
            var result = await _applicationsRepository.DeleteComponentTypeEntity(componentTypeId);

            return result;
        }

        public async Task<ComponentType> GetComponentTypeById(Guid componentTypeId)
        {
            var result = await _applicationsRepository.GetComponentTypeById(componentTypeId);

            return result;
        }

        public async Task<List<ComponentType>> GetComponentTypes()
        {
            var result = await _applicationsRepository.GetAllComponentTypes();

            return result;
        }

        public async Task<Guid> InsertComponentType(ComponentTypeRequest componentType)
        {
            var temp = new ComponentType() { Name = componentType.Name, Active = componentType.Active };

            var result = await _applicationsRepository.InsertComponentTypeEntity(temp);

            return result;
        }

        public async Task<bool> UpdateComponentType(Guid componentTypeId, ComponentTypeRequest componentType)
        {
            var temp = new ComponentType() { Id= componentTypeId, Name = componentType.Name, Active = componentType.Active };

            var result = await _applicationsRepository.UpdateComponentTypeEntity(temp);

            return result;
        }
    }
}
