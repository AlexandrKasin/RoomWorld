using System;

namespace Service.dto
{
    public class OrderedFlatViewModel
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public double Price { get; set; }

        public FlatViewModel Flat { get; set; }

        public UserViewModel User { get; set; }
    }
}
