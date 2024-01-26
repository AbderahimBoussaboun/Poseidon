using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Node
{
    public Guid NodeId { get; set; }

    public bool Active { get; set; }

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Description { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<NodePool> NodePools { get; set; } = new List<NodePool>();
}
