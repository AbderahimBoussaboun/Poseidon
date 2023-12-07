using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Applications
{
    public interface IApplicationsRepository
    {
        #region APPLICATIONS
        public Task<List<Application>> GetAllApplications();
        public Task<Application> GetApplicationById(Guid applicationId);
        public Task<List<SubApplication>> GetAllSubApplicationsByApplicationId(Guid applicationId);
        public Task<SubApplication> GetSubApplicationByApplicationId(Guid applicationId, Guid subApplicationId);
        public  Task<List<Component>> GetAllComponentsBySubApplicationId(Guid applicationId, Guid subApplicationId);
        public  Task<Component> GetComponentBySubApplicationId(Guid applicationId, Guid subApplicationId, Guid idComponent);
        public Task<Guid> InsertApplicationEntity(Application application);
        public Task<bool> UpdateApplicationEntity(Application application);
        public Task<bool> DeleteApplicationEntity(Guid applicationId);
        #endregion

        #region SUBAPPLICATIONS
        public Task<List<SubApplication>> GetAllSubApplications();
        public Task<SubApplication> GetSubApplicationById(Guid id);
        public Task<Guid> InsertSubApplicationEntity(Guid applicationId, SubApplication subApplication);
        public Task<bool> UpdateSubApplicationEntity(Guid applicationId, Guid subApplicationId,SubApplication subApplication);
        public Task<bool> DeleteSubApplicationEntity(Guid applicationId, Guid subApplicationId);
        #endregion

        #region COMPONENTS
        public Task<List<Component>> GetAllComponents();
        public Task<Component> GetComponentById(Guid componentId);
        public Task<Guid> InsertComponentEntity(Guid applicationId, Guid subApplicationId, Component component);
        public Task<bool> UpdateComponentEntity(Guid applicationId, Guid subApplicationId, Guid componentId,Component component);
        public Task<bool> DeleteComponentEntity(Guid applicationId, Guid subApplicationId, Guid componentId);
        #endregion


        #region COMPONENTSTYPE
        public Task<List<ComponentType>> GetAllComponentTypes();
        public Task<ComponentType> GetComponentTypeById(Guid id);
        public Task<Guid> InsertComponentTypeEntity(ComponentType t);
        public Task<bool> UpdateComponentTypeEntity(ComponentType t);
        public Task<bool> DeleteComponentTypeEntity(Guid id);
        #endregion



    }
}
