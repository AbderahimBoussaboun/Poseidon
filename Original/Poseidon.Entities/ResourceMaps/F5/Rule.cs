using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Rule : BaseEntity
    {
        public Rule() { }
        public Guid VirtualId { get; set; } // Agregamos una propiedad para la clave foránea
        public Virtual Virtual { get; set; } // Agregamos una navegación a Virtual
        public virtual ICollection<Irule> Irules { get; set; } // Renombramos la propiedad a "Irules"
    }
}
