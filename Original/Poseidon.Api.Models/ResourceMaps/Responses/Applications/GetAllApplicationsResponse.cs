using Poseidon.Entities.ResourceMaps.Applications;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Applications
{
    public class GetAllApplicationsResponse
    {
        public List<Application> Applications { get; set; }
    }
}
