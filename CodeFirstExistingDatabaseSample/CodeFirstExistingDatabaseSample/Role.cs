using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Role
{
    public Guid RoleId { get; set; }

    public int Type { get; set; }

    public Guid ServerId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Server Server { get; set; } = null!;

    public virtual ICollection<ServerApplication> ServerApplications { get; set; } = new List<ServerApplication>();
}
