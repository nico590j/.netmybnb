using System;

namespace Mybnb.api.Models
{
    public class TenantPeriod
    {
        public int TenantPeriodID { get; set; }

        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }

        public User Tenant { get; set; }
    }
}
