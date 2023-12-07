using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Irule
{
    public Guid IruleId { get; set; }

    public bool Active { get; set; }

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Name { get; set; } = null!;

    public string? Redirect { get; set; }

    public Guid? RuleId { get; set; }

    public virtual Rule? Rule { get; set; }
}
