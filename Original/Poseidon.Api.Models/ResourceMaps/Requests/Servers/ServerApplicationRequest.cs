using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Servers
{
    public class ServerApplicationRequest : BaseRequest
    {
        public Guid RoleId { get; set; }
    }
}
