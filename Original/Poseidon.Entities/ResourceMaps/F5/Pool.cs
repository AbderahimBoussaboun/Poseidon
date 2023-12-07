using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Pool : BaseEntity
    {
        public Pool() { }
        public string Description { get; set; }
        public string BalancerType { get; set; }
        public Guid? MonitorId { get; set; } // Cambia a Guid? para permitir valores nulos
        public Monitor Monitor { get; set; } // Propiedad de navegación para la relación con Monitor
        public Guid VirtualId { get; set; } // Agregamos una propiedad para la clave foránea
        public Virtual Virtual { get; set; } // Agregamos una navegación a Virtual
        public virtual ICollection<Node> Members { get; set; }
    }
}
