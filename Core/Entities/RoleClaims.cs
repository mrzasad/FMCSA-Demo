using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class RoleClaims
    {
        public RoleClaims()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public long RoleClaimId { get; set; }
        public long RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
