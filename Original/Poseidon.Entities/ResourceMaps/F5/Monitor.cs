using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Monitor : BaseEntity
    {
        public Monitor() { }

        public string Adaptive { get; set; }
        public string Cipherlist { get; set; }
        public string Compatibility { get; set; }
        public string Debug { get; set; }
        public string Defaults_from { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public string get { get; set; }
        public string Interval { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Service { get; set; }
        public string IP_DSCP { get; set; }
        public string RECV { get; set; }
        public string RECV_disable { get; set; }
        public string Reverse { get; set; }
        public string SEND { get; set; }
        public string ssl_profile { get; set; }
        public string time_until_up { get; set; }
        public string timeout { get; set; }
        public string username { get; set; }
    }
}
