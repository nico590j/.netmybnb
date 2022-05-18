using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mybnb.dtolibrary.DTOs.BNB
{
    public class PossibleRentingPeriodResponse
    {
        public int PossibleRentingPeriodID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DailyPrice { get; set; }
        public int MinimumRentingDays { get; set; }
    }
}
