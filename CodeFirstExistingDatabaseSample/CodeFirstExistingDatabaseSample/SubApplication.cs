using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class SubApplication
{
    public Guid SubApplicationId { get; set; }

    public Guid ApplicationId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
