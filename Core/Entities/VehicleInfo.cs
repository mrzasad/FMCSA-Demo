

namespace Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Core.Entities.Common;
    public class VehicleInfo: BaseEntity
    {
        public long VehicleInfoId { get; set; }
        public int Type { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string DOTNumber { get; set; }
        public string SafetyLink { get; set; }

    }
}
