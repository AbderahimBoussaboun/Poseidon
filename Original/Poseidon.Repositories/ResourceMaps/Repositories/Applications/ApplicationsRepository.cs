using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Applications
{
    public class ApplicationsRepository : GenericRepository, IApplicationsRepository
    {
        private DataContext _context;
        public ApplicationsRepository(DataContext context) : base(context) {
            _context = context;
        }


        #region EXPRESSIONS_INCLUDE

        Expression<Func<Application, object>>[] includesApplication = new Expression<Func<Application, object>>[]
          {
                e => e.SubAplications
          };


        Expression<Func<SubApplication, object>>[] includesSubApplication = new Expression<Func<SubApplication, object>>[]
         {
                e => e.Components
         };


        Expression<Func<Component, object>>[] includesComponent = new Expression<Func<Component, object>>[]
        {
                
        };

        Expression<Func<ComponentType, object>>[] includesComponentType = new Expression<Func<ComponentType, object>>[]
       {

       };

        #endregion


        #region APPLICATIONS


        public async Task<bool> DeleteApplicationEntity(Guid applicationId)
        {
            var result = await base.DeleteEntity<Application>(applicationId);
            return result;
        }

        public async Task<List<Application>> GetAllApplications()
        {
            var result = await base.GetAll(includesApplication);
            return result;
        }

        public async Task<Application> GetApplicationById(Guid applicationId)
        {
            var result = await base.GetById(applicationId, includesApplication);
            return result;
        }

        public async Task<List<SubApplication>> GetAllSubApplicationsByApplicationId(Guid applicationId)
        {
            try
            {
                var result = await base.GetById(applicationId, includesApplication);
                return result.SubAplications.ToList();
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public async Task<SubApplication> GetSubApplicationByApplicationId(Guid applicationId, Guid subApplicationId)
        {
            try
            {
                var result = await base.GetById(applicationId, includesApplication);
                var subApplication = result.SubAplications.SingleOrDefault(s => s.Id.Equals(subApplicationId));
                return subApplication;
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public async Task<List<Component>> GetAllComponentsBySubApplicationId(Guid applicationId, Guid subApplicationId)
        {
            try
            {
                var subApplication = await base.GetById(subApplicationId, includesSubApplication);
                var result = subApplication.ApplicationId.Equals(applicationId) ? subApplication.Components.ToList() : new List<Component>();
                return result;
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public async Task<Component> GetComponentBySubApplicationId(Guid applicationId, Guid subApplicationId,Guid idComponent)
        {
            try
            {
                 var application = await base.GetById(applicationId, includesApplication);
                 var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                 var subApplication =  await base.GetById(subApplicationLocal.Id, includesSubApplication);
                 var componentLocal =  subApplication.Components.SingleOrDefault(s => s.Id == idComponent) ;
                 var componet = await base.GetById(componentLocal.Id, includesComponent) ;
                
                return componet;
                
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public async Task<Guid> InsertApplicationEntity(Application application)
        {
            var result = await base.InsertEntity(application);
            return result;
        }

        public async Task<bool> UpdateApplicationEntity(Application application)
        {
            var result = await base.UpdateEntity(application);
            return result;
        }
        #endregion

        #region SUBAPPLICATIONS


        public async Task<List<SubApplication>> GetAllSubApplications()
        {
            var result = await base.GetAll(includesSubApplication);
            return result;
        }

        public async Task<SubApplication> GetSubApplicationById(Guid subApplicationId)
        {
            var result = await base.GetById(subApplicationId, includesSubApplication);
            return result;
        }

        public async Task<Guid> InsertSubApplicationEntity(Guid applicationId, SubApplication subApplication)
        {
            var application = await base.GetById(applicationId, includesApplication);
            if (application == null) return Guid.Empty;

            var result = await base.InsertEntity(subApplication);
            return result;
        }

        public async Task<bool> UpdateSubApplicationEntity(Guid applicationId, Guid subApplicationId, SubApplication subApplication)
        {
            try
            {
                var application = await base.GetById(applicationId, includesApplication);
                var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                if (subApplicationLocal == null) return false;

                var result = await base.UpdateEntity(subApplication);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
            
           
        }

        public async Task<bool> DeleteSubApplicationEntity(Guid applicationId, Guid subApplicationId)
        {
            try
            {
                var application = await base.GetById(applicationId, includesApplication);
                var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                if (subApplicationLocal == null) return false;

                var result = await base.DeleteEntity<SubApplication>(subApplicationId);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        #endregion

        #region COMPONENTS

        public async Task<List<Component>> GetAllComponents()
        {
            var result = await base.GetAll(includesComponent);
            return result;
        }

        public async Task<Component> GetComponentById(Guid componentId)
        {
            var result = await base.GetById(componentId, includesComponent);
            return result;
        }

        public async Task<Guid> InsertComponentEntity(Guid applicationId, Guid subApplicationId, Component component)
        {
            try
            {
                var application = await base.GetById(applicationId, includesApplication);
                var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                var subApplication = await base.GetById(subApplicationLocal.Id, includesSubApplication);
                if (subApplication == null) return Guid.Empty;
                var result = await base.InsertEntity(component);
                return result;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
            
        }

        public async Task<bool> UpdateComponentEntity(Guid applicationId, Guid subApplicationId, Guid componentId, Component component)
        {

            try
            {
                var application = await base.GetById(applicationId, includesApplication);
                var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                var subApplication = await base.GetById(subApplicationLocal.Id, includesSubApplication);
                var componentLocal = subApplication.Components.SingleOrDefault(s => s.Id == componentId);
                var componentTemp = await base.GetById(componentLocal.Id, includesComponent);
                if (component == null) return false;
                var result = await base.UpdateEntity(component);
                return result;
            }
            catch (Exception e)
            {
                return false;
            }

            
        }

        public async Task<bool> DeleteComponentEntity(Guid applicationId, Guid subApplicationId, Guid componentId)
        {
            try
            {
                var application = await base.GetById(applicationId, includesApplication);
                var subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                var subApplication = await base.GetById(subApplicationLocal.Id, includesSubApplication);
                var componentLocal = subApplication.Components.SingleOrDefault(s => s.Id == componentId);
                var component = await base.GetById(componentLocal.Id, includesComponent);
                if (component == null) return false;
                
                var result = await base.DeleteEntity<Component>(componentId);
                return result;
            }
            catch (Exception e)
            {
                return false;
              
            }

         
        }


        #endregion


        #region COMPONENTTYPE

        public async Task<List<ComponentType>> GetAllComponentTypes()
        {
            var result = await base.GetAll(includesComponentType);
            return result;
        }

        public async Task<ComponentType> GetComponentTypeById(Guid id)
        {
            var result = await base.GetById(id, includesComponentType);
            return result;
        }

        public async Task<Guid> InsertComponentTypeEntity(ComponentType t)
        {
            var result = await base.InsertEntity(t);
            return result;
        }

        public async Task<bool> UpdateComponentTypeEntity(ComponentType t)
        {
            var result = await base.UpdateEntity(t);
            return result;
        }

        public async Task<bool> DeleteComponentTypeEntity(Guid id)
        {
            var result = await base.DeleteEntity<ComponentType>(id);
            return result;
        }


        #endregion

        #region SHARED

        private async Task<string> CheckDependencies(Guid? applicationId, Guid? subApplicationId, Guid? componentId) {


            Application application = null;
            SubApplication subApplicationLocal = null;
            SubApplication subApplication=null;
            Component componentLocal = null;
            Component component = null;

           
            try
            {
                if (applicationId == null) return "applicationId";
                application = await base.GetById(applicationId.Value, includesApplication);

            }
            catch (Exception)
            {

                throw;
            }


            try
            {
                if (subApplicationId != null) {
                    subApplicationLocal = application.SubAplications.SingleOrDefault(s => s.Id == subApplicationId);
                    if (subApplicationLocal == null) return "subApplicationId";
                    subApplication = await base.GetById(subApplicationLocal.Id, includesSubApplication);
                    if (subApplication == null) return "subApplicationId";
                }
                
            }
            catch (Exception)
            {
                return "subApplicationId";
            }

            try
            {
                if (componentId != null) {
                    componentLocal = subApplication.Components.SingleOrDefault(s => s.Id == componentId);
                    if (componentLocal == null) return "componentId";
                    component = await base.GetById(componentLocal.Id, includesComponent);
                    if (component == null) return "componentId";

                }
                
            }
            catch (Exception)
            {
                return "componentId";
            }
            return string.Empty;

        }

        #endregion

    }
}
