using System;
using System.Collections.Generic;
using System.Text;

namespace Service.dto
{
    public class OrderViewModel
    {
        
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public double Price { get; set; }
    }
}
