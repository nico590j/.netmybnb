using Mybnb.dtolibrary.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mybnb.dtolibrary.DTOs.BNB
{
    public class BNBRequest
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public BNBTypes TypeOfEstablishment { get; set; }
    }
}
