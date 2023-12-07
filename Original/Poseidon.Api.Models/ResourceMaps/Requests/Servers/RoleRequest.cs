using Poseidon.Entities.ResourceMaps.Enums;
using System;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Servers
{
    public class RoleRequest : BaseRequest
    {
        public TypeRole Type { get; set; }
        public Guid ServerId { get; set; }
    }
}
