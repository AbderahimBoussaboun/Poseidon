using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Pool : BaseEntity
    {
        public Pool() { }
        public string Description { get; set; }
        public string BalancerType { get; set; }
        public Guid? MonitorId { get; set; } // Cambia a Guid? para permitir valores nulos // Propiedad de navegación para la relación con Monitor
        public Guid VirtualId { get; set; } // Agregamos una propiedad para la clave foránea
    }
}
