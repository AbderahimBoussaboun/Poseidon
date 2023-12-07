using Poseidon.Entities.ResourceMaps.Products;
using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Server:BaseEntity
    {

        public Server() { }
        public String Location { get; set; } //se puede cambiar a un enum o tablas de posibles localizaciones.
        public String Ip { get; set; }
        public String OS { get; set; }
        public Guid EnvironmentId { get; set; }
        public Guid ProductId { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual Product Product { get; set; }
        public virtual Environment Environment { get; set; }
        

    }

}
