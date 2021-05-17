using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            RoleClaims = new HashSet<RoleClaims>();
        }

        public long RoleId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<RoleClaims> RoleClaims { get; set; }
    }
}
