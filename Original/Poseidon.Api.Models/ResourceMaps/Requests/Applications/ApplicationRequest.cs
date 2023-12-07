using System;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Applications
{
    public class ApplicationRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
    }
}
