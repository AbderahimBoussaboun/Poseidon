using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications
{
    public interface IComponentTypesService
    {
        public Task<List<ComponentType>> GetComponentTypes();
        public Task<ComponentType> GetComponentTypeById(Guid componentTypeId);
        public Task<Guid> InsertComponentType(ComponentTypeRequest componentType);
        public Task<bool> DeleteComponentType(Guid componentTypeId);
        public Task<bool> UpdateComponentType(Guid componentTypeId, ComponentTypeRequest componentType);

    }
}
