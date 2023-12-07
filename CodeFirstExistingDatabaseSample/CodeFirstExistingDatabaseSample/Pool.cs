using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Pool
{
    public Guid PoolId { get; set; }

    public Guid? MonitorId { get; set; }

    public bool Active { get; set; }

    public string BalancerType { get; set; } = null!;

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public virtual Monitor? Monitor { get; set; }

    public virtual ICollection<NodePool> NodePools { get; set; } = new List<NodePool>();

    public virtual ICollection<Virtual> Virtuals { get; set; } = new List<Virtual>();
}
