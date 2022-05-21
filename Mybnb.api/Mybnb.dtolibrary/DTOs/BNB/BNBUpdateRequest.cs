using Mybnb.dtolibrary.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mybnb.dtolibrary.DTOs.BNB
{
    public class BNBUpdateRequest
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<BNBImageDTO> Images { get; set; }

        public List<PossibleRentingPeriodResponse> RentingPeriods { get; set; }
        public List<TenantPeriodResponse> TenantPeriods { get; set; }

        public BNBTypes TypeOfEstablishment { get; set; }
        public UserResponse Owner { get; set; }
    }
}
