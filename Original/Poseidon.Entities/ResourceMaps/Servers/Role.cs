// Decompiled with JetBrains decompiler
// Type: Poseidon.Entities.ResourceMaps.Servers.Role
// Assembly: Poseidon.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7ACB5B1D-C548-4AE3-B373-446A658C5F79
// Assembly location: C:\Users\abderahim.boussaboun\Downloads\backup_2024-03-20\Poseidon.Entities.dll

using Poseidon.Entities.ResourceMaps.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Role : BaseEntity
    {
        public TypeRole Type { get; set; }

        public Guid ServerId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual Server Server { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual ICollection<ServerApplication> ServerApplications { get; set; }
    }
}
