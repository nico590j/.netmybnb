﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mybnb.dtolibrary.DTOs.BNB
{
    public class CreateBNB
    {
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<BNBImageDTO> Images { get; set; }
    }
}
