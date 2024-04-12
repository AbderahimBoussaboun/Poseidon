using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Infrastructure : BaseEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual ICollection<Server> Servers { get; set; }
    }
}