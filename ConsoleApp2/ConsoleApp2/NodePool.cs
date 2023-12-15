using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class NodePool
{
    public Guid NodeId { get; set; }

    public Guid PoolId { get; set; }

    public string NodePort { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Name { get; set; } = null!;

    public virtual Node Node { get; set; } = null!;

    public virtual Pool Pool { get; set; } = null!;
}
