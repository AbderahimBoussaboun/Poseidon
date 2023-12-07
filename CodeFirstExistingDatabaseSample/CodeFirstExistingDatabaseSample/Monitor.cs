using System;
using System.Collections.Generic;

namespace CodeFirstExistingDatabaseSample;

public partial class Monitor
{
    public Guid MonitorId { get; set; }

    public bool Active { get; set; }

    public string Adaptive { get; set; } = null!;

    public string Cipherlist { get; set; } = null!;

    public string Compatibility { get; set; } = null!;

    public DateTime? DateDisable { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateModify { get; set; }

    public string Debug { get; set; } = null!;

    public string DefaultsFrom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public string IpDscp { get; set; } = null!;

    public string Interval { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Recv { get; set; } = null!;

    public string RecvDisable { get; set; } = null!;

    public string Reverse { get; set; } = null!;

    public string Send { get; set; } = null!;

    public string Server { get; set; } = null!;

    public string Service { get; set; } = null!;

    public string Get { get; set; } = null!;

    public string SslProfile { get; set; } = null!;

    public string TimeUntilUp { get; set; } = null!;

    public string Timeout { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();
}
