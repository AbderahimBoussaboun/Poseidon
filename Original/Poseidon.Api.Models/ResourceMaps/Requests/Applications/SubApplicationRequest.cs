using System;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Applications
{
    public class SubApplicationRequest : BaseRequest
    {
        public Guid ApplicationId { get; set; }
    }
}
