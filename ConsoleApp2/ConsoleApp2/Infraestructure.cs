using System;
using System.Collections.Generic;

namespace ConsoleApp2;

public partial class Infraestructure
{
    public Guid InfraestructureId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public DateTime? DateDisable { get; set; }

    public bool Active { get; set; }
}
