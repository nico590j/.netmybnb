using System;

namespace Mybnb.api.Models
{
    public class PossibleRentingPeriod
    {
        public int PossibleRentingPeriodID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DailyPrice { get; set; }
        public int MinimumRentingDays { get; set; }
    }
}
