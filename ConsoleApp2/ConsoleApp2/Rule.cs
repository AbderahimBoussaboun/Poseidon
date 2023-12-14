using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Rule
{
    public Guid RuleId { get; set; }

    public bool Active { get; set; }

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Name { get; set; } = null!;

    public Guid? VirtualId { get; set; }

    public virtual ICollection<Irule> Irules { get; set; } = new List<Irule>();

    public virtual Virtual? Virtual { get; set; }
}
