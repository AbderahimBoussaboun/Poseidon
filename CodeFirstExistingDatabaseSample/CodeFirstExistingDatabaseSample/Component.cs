using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Component
{
    public Guid ComponentId { get; set; }

    public string? Ip { get; set; }

    public string? Ports { get; set; }

    public string? QueryString { get; set; }

    public Guid SubApplicationId { get; set; }

    public Guid? BalancerId { get; set; }

    public Guid ComponentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Balancer? Balancer { get; set; }

    public virtual ComponetType ComponentType { get; set; } = null!;

    public virtual ServerApplication? ServerApplication { get; set; }

    public virtual SubApplication SubApplication { get; set; } = null!;
}
