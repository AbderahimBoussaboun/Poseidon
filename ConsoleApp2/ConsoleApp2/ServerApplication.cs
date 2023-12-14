using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class ServerApplication
{
    public Guid ServerApplicationId { get; set; }

    public Guid RoleId { get; set; }

    public Guid? ComponentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }

    public virtual Component? Component { get; set; }

    public virtual Role Role { get; set; } = null!;
}
