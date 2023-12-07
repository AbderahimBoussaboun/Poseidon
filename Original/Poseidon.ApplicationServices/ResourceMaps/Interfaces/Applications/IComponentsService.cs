using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications
{
    public interface IComponentsService
    {
        public Task<List<Component>> GetComponents();
        public Task<Component> GetComponentById(Guid id);
        public Task<Guid> InsertComponent(Guid applicationId,Guid subApplicationId, ComponentRequest component);
        public Task<bool> DeleteComponent(Guid applicationId, Guid subApplicationId, Guid componentId);
        public Task<bool> UpdateComponent(Guid applicationId, Guid subApplicationId, Guid componentId, ComponentRequest component);
    }
}
