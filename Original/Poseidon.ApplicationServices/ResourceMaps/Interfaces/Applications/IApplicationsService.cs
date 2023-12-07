using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications
{
    public interface IApplicationsService
    {
        public Task<List<Application>> GetApplications();
        public Task<Application> GetApplicationById(Guid id);
        public Task<List<SubApplication>> GetAllSubApplicationsByApplicationId(Guid id);
        public Task<SubApplication> GetSubApplicationByApplicationId(Guid applicationId, Guid subApplicationId);
        public Task<List<Component>> GetAllComponentsBySubApplicationId(Guid applicationId, Guid subApplicationId);
        public Task<Component> GetComponentBySubApplicationId(Guid applicationId, Guid subApplicationId, Guid idComponent);
        public Task<Guid> InsertApplication(ApplicationRequest application);
        public Task<bool> DeleteApplication(Guid id);
        public Task<bool> UpdateApplication(ApplicationRequest application);
    }
}
