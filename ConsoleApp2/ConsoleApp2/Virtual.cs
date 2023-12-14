using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Virtual
{
    public Guid VirtualId { get; set; }

    public bool Active { get; set; }

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Name { get; set; } = null!;

    public Guid? PoolId { get; set; }

    public virtual Pool? Pool { get; set; }

    public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
}
