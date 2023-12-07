using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.Entities.ResourceMaps.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.ResourceMaps.Applications
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ILogger<ApplicationsController> _logger;
        private readonly IApplicationsService _applicationsService;
        private readonly ISubApplicationsService _subApplicationsService;
        private readonly IComponentsService _componentsService;

        public ApplicationsController(ILogger<ApplicationsController> logger, IApplicationsService applicationsService,
            IComponentsService componentsService, ISubApplicationsService subApplicationsService)
        {
            _logger = logger;
            _applicationsService = applicationsService;
            _subApplicationsService = subApplicationsService;
            _componentsService = componentsService;
        }


        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Application>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllApplications()
        {
            var result= await _applicationsService.GetApplications();
            return Ok(result);
        }

        [HttpGet("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicationById(Guid applicationId)
        {
            var result= await _applicationsService.GetApplicationById(applicationId);
            return Ok(result);
        }


        [HttpGet("{applicationId}/SubApplications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubApplication>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getAllSubApplicationsByApplicationId(Guid applicationId)
        {
            var result = await _applicationsService.GetAllSubApplicationsByApplicationId(applicationId);
            return Ok(result);
        }

        [HttpGet("{applicationId}/SubApplications/{subApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubApplication))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getSubApplicationByIdApplication(Guid applicationId, Guid subApplicationId)
        {
            var result = await _applicationsService.GetSubApplicationByApplicationId(applicationId, subApplicationId);
            return Ok(result);
        }


        [HttpGet("{applicationId}/SubApplications/{subApplicationId}/Components")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Component>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getAllComponentsByIdSubApplication(Guid applicationId, Guid subApplicationId)
        {
            var result = await _applicationsService.GetAllComponentsBySubApplicationId(applicationId,subApplicationId);
            
            return Ok(result);
        }

        [HttpGet("{applicationId}/SubApplications/{subApplicationId}/Components/{componentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubApplication>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getComponentByIdSubApplication(Guid applicationId, Guid subApplicationId, Guid componentId)
        {
            var result = await _applicationsService.GetComponentBySubApplicationId(applicationId,subApplicationId,componentId);

            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertApplication([FromBody] ApplicationRequest application)
        {
            var result= await _applicationsService.InsertApplication(application);
            return Ok(result);
        }

        [HttpPost("{applicationId}/SubApplications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertSubApplication(Guid applicationId,[FromBody] SubApplicationRequest subApplication)
        {
            var result = await _subApplicationsService.InsertSubApplication(applicationId,subApplication);
            return Ok(result);
        }

        [HttpPost("{applicationId}/SubApplications/{subApplicationId}/Components")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertComponent(Guid applicationId,Guid subApplicationId,[FromBody] ComponentRequest componet)
        {
            var result = await _componentsService.InsertComponent(applicationId,subApplicationId, componet);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplication(Guid applicationId,[FromBody] ApplicationRequest application)
        {
            var result= await _applicationsService.UpdateApplication(application);
            return Ok(result);
        }

        [HttpPut("{applicationId}/SubApplications/{subApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSubApplication(Guid applicationId,Guid subApplicationId, [FromBody] SubApplicationRequest subApplication)
        {
            var result = await _subApplicationsService.UpdateSubApplication(applicationId, subApplicationId,subApplication);
            return Ok(result);
        }


        [HttpPut("{applicationId}/SubApplications/{subApplicationId}/Components/{componentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateComponent(Guid applicationId, Guid subApplicationId, Guid componentId, [FromBody] ComponentRequest component)
        {
            var result = await _componentsService.UpdateComponent(applicationId, subApplicationId, componentId,component);
            return Ok(result);
        }


        #endregion

        #region DELETE
        [HttpDelete("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplication(Guid applicationId)
        {
            var result= await _applicationsService.DeleteApplication(applicationId);
            return Ok(result);
        }

        [HttpDelete("{applicationId}/SubApplications/{subApplicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubApplication(Guid applicationId, Guid subApplicationId)
        {
            var result = await _subApplicationsService.DeleteSubApplication(applicationId, subApplicationId);
            return Ok(result);
        }

        [HttpDelete("{applicationId}/SubApplications/{subApplicationId}/Components/{componentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComponent(Guid applicationId, Guid subApplicationId, Guid componentId)
        {
            var result = await _componentsService.DeleteComponent(applicationId, subApplicationId, componentId);
            return Ok(result);
        }



        #endregion

        #region OTHER
        #endregion
    }
}
