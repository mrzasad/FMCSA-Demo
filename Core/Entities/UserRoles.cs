using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class UserRoles
    {
        public long UserId { get; set; }
        public long RoleClaimId { get; set; }

        public virtual RoleClaims RoleClaim { get; set; }
        public virtual Users User { get; set; }
    }
}
