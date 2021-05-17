using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class UserClaims
    {
        public int ClaimId { get; set; }
        public long UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual Users User { get; set; }
    }
}
