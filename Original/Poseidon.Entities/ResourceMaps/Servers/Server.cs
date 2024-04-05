// Decompiled with JetBrains decompiler
// Type: Poseidon.Entities.ResourceMaps.Servers.Server
// Assembly: Poseidon.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7ACB5B1D-C548-4AE3-B373-446A658C5F79
// Assembly location: C:\Users\abderahim.boussaboun\Downloads\backup_2024-03-20\Poseidon.Entities.dll

using Poseidon.Entities.ResourceMaps.Products;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Server : BaseEntity
    {
        public string Location { get; set; }

        public string Ip { get; set; }

        public string OS { get; set; }

        public Guid EnvironmentId { get; set; }

        public Guid ProductId { get; set; }

        public Guid InfrastructureId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual ICollection<Role> Roles { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual Product Product { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual Environment Environment { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual Infrastructure Infrastructure { get; set; }
    }
}
