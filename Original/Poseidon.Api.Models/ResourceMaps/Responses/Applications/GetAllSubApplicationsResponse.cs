
using Poseidon.Entities.ResourceMaps.Applications;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Applications
{
    public class GetAllSubApplicationsResponse
    {
        public List<SubApplication> SubApplications { get; set; }
    }
}
