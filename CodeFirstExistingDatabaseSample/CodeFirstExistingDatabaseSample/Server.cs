using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Server
{
    public Guid ServerId { get; set; }

    public string? Location { get; set; }

    public string? Ip { get; set; }

    public string? Os { get; set; }

    public Guid EnvironmentId { get; set; }

    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Environment Environment { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
