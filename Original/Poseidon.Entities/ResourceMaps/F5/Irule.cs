using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Irule : BaseEntity
    {
        public Irule() { }
        public string Redirect { get; set; }
        public Guid RuleId { get; set; } // Agregamos una propiedad para la clave foránea
        public Rule Rule { get; set; } // Agregamos una navegación a Rule
    }
}
