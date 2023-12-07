using Poseidon.Entities.ResourceMaps.Applications;
using System;

namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class ServerApplication:BaseEntity
    {
        public ServerApplication() { } 
        public Guid RoleId { get; set; }
        public Guid? ComponentId { get; set; }
        public Component Component { get; set; }
        public virtual Role Role { get; set; }
    }
}
