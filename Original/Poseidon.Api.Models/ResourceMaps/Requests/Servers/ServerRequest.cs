using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Servers
{
    public class ServerRequest:BaseRequest
    {
        public String Location { get; set; } //se puede cambiar a un enum o tablas de posibles localizaciones.
        public String Ip { get; set; }
        public String OS { get; set; }
        public Guid EnvironmentId { get; set; }
        public Guid ProductId { get; set; }
    }
}
