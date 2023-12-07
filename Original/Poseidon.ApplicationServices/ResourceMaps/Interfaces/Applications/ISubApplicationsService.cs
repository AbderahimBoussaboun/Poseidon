using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications
{
    public interface ISubApplicationsService
    {
        public Task<List<SubApplication>> GetSubApplications();
        public Task<SubApplication> GetSubApplicationById(Guid subApplicationId);
        public Task<Guid> InsertSubApplication(Guid applicationId, SubApplicationRequest subApplication);
        public Task<bool> DeleteSubApplication(Guid applicationId, Guid subApplicationId);
        public Task<bool> UpdateSubApplication(Guid applicationId, Guid subApplicationId, SubApplicationRequest subApplication);
    }
}
