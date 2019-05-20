using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO.ApartmentDTO
{
    public class ApartmentFiltersDTO
    {
        public int? MaxCost { get; set; }
        public int? MinCost { get; set; }
        public int? AmountBathrooms { get; set; }
        public int? AmountBedrooms { get; set; }
    }
}