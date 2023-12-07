using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Applications
{
    public class SubApplicationsService : ISubApplicationsService
    {
        private readonly IApplicationsRepository _applicationsRepository;

        public SubApplicationsService(IApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }

        public async Task<bool> DeleteSubApplication(Guid applicationId, Guid subApplicationId)
        {
            var resultApplication = await _applicationsRepository.GetApplicationById(applicationId);
            if (resultApplication == null) return false;

            var result = await _applicationsRepository.DeleteSubApplicationEntity(applicationId, subApplicationId);
            return result;
        }

        public async Task<SubApplication> GetSubApplicationById( Guid subApplicationId)
        {
            var result = await _applicationsRepository.GetSubApplicationById(subApplicationId);
            return result;
        }

        public async Task<List<SubApplication>> GetSubApplications()
        {
            var result = await _applicationsRepository.GetAllSubApplications();
            return result;
        }

        public async Task<Guid> InsertSubApplication(Guid applicationId,SubApplicationRequest subApplication)
        {
            var temp = new SubApplication() { 
                Name = subApplication.Name,
                ApplicationId = applicationId,
                Active = subApplication.Active
            };  
            var result= await _applicationsRepository.InsertSubApplicationEntity(applicationId,temp);
            return result;
        }

        public async Task<bool> UpdateSubApplication(Guid applicationId, Guid subApplicationId, SubApplicationRequest subApplication)
        {

            var temp = new SubApplication() { 
                Id = subApplication.Id, 
                Name = subApplication.Name,
                ApplicationId=subApplication.ApplicationId,
                Active = subApplication.Active
            };

            var result =await _applicationsRepository.UpdateSubApplicationEntity(applicationId, subApplicationId,temp);
            return result;
        }
    }
}
