using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();
}
