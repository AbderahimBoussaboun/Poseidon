using Poseidon.Entities.ResourceMaps.Servers;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Servers
{
    public class GetAllServersResponse
    {
        public List<Server> Servers { get; set; }
    }
}
