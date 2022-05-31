using Mybnb.dtolibrary.DTOs;
using System.Collections.Generic;

namespace Mybnb.api.Models
{
    public class BNB
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<BnbImage> Images { get; set; }
        public List<PossibleRentingPeriod> RentingPeriods { get; set; }
        public List<TenantPeriod> TenantPeriods { get; set; }

        public BNBTypes TypeOfEstablishment{ get; set; }
        public User Owner { get; set; }
    }
}
