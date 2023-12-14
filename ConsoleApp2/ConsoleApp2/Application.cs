using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Application
{
    public Guid ApplicationId { get; set; }

    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<SubApplication> SubApplications { get; set; } = new List<SubApplication>();
}
