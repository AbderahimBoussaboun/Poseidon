
using Poseidon.Entities.ResourceMaps.Servers;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Servers
{
    public class GetAllServerApplicationsResponse
    {
        public List<ServerApplication> ServerApplications { get; set; }
    }
}
