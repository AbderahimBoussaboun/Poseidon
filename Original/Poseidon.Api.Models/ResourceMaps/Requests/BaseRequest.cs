using System;

namespace Poseidon.Api.Models.ResourceMaps.Requests
{
    public class BaseRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
