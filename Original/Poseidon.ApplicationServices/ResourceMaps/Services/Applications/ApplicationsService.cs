using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Applications
{
   public class ApplicationsService : IApplicationsService
    {
        private readonly IApplicationsRepository _applicationRepository;

        public ApplicationsService(IApplicationsRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task<bool> DeleteApplication(Guid id)
        {
            var result=await _applicationRepository.DeleteApplicationEntity(id);
           
            return result;
        }

        public async Task<Application> GetApplicationById(Guid id)
        {
            var result = await _applicationRepository.GetApplicationById(id);
            return result;
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = await _applicationRepository.GetAllApplications();
            return result;
        }

        public async Task<List<SubApplication>> GetAllSubApplicationsByApplicationId(Guid id)
        {
            var result = await _applicationRepository.GetAllSubApplicationsByApplicationId(id);
            return result;
        }
        public async Task<SubApplication> GetSubApplicationByApplicationId(Guid applicationId, Guid subApplicationId) {
            var result = await _applicationRepository.GetSubApplicationByApplicationId(applicationId, subApplicationId);
            return result;
        }
        public async Task<List<Component>> GetAllComponentsBySubApplicationId(Guid applicationId, Guid subApplicationId)
        {
            var result = await _applicationRepository.GetAllComponentsBySubApplicationId(applicationId, subApplicationId);
            return result;
        }

        public async Task<Component> GetComponentBySubApplicationId(Guid applicationId, Guid subApplicationId, Guid idComponent)
        {
            var result = await _applicationRepository.GetComponentBySubApplicationId(applicationId, subApplicationId, idComponent);
            return result;
        }

        public async Task<Guid> InsertApplication(ApplicationRequest application)
        {
            var temp = new Application() { Name = application.Name,ProductId=application.ProductId,Active=application.Active };
            var result =await _applicationRepository.InsertApplicationEntity(temp);
            return result;
        }

        public async Task<bool> UpdateApplication(ApplicationRequest application)
        {
            var temp = new Application() { Id = application.Id, Name = application.Name ,ProductId=application.ProductId, Active = application.Active };
            var result = await _applicationRepository.UpdateApplicationEntity(temp);
            return result;
        }

    }
}
