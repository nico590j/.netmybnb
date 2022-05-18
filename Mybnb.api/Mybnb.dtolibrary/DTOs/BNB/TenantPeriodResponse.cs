using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mybnb.dtolibrary.DTOs.BNB
{
    public class TenantPeriodResponse
    {
        public int TenantPeriodID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AuthenticateResponse Tenant { get; set; }
    }
}
