using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5
{

    public partial class Monitor:BaseEntity
    {
        public string Adaptive { get; set; }
        public string Cipherlist { get; set; }
        public string Compatibility { get; set; }
        public string Debug { get; set; }
        public string Defaults_from { get; set; }  // changed from DefaultsFrom
        public string Description { get; set; }
        public string Destination { get; set; }
        public string IP_DSCP { get; set; }  // changed from IpDscp
        public string Interval { get; set; }
        public string Password { get; set; }
        public string RECV { get; set; }  // changed from Recv
        public string RECV_disable { get; set; }  // changed from RecvDisable
        public string Reverse { get; set; }
        public string SEND { get; set; }  // changed from Send
        public string Server { get; set; }
        public string Service { get; set; }
        public string get { get; set; }  // changed from Get
        public string ssl_profile { get; set; }  // changed from SslProfile
        public string time_until_up { get; set; } //changed from TimeUntilUp
        public string timeout { get; set; } //changed from Timeout
        public string username { get; set; }  // changed from Username
        public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    }
}
