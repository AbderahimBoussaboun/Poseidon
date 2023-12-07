using System;

namespace Poseidon.Entities.ResourceMaps
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime? DateInsert { get; set; }
        public DateTime? DateModify { get; set; }
        public DateTime? DateDisable { get; set; }
        public bool Active { get; set; }
    }
}
