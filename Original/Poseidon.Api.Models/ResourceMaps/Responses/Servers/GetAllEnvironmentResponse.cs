
using Poseidon.Entities.ResourceMaps.Servers;
using System.Collections.Generic;


namespace Poseidon.Api.Models.ResourceMaps.Responses.Servers
{
    public class GetAllEnvironmentResponse
    {
        public List<Environment> Environments { get; set; }
    }
}
