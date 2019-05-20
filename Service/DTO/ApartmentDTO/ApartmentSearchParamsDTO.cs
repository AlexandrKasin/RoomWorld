using System;

namespace Service.DTO.ApartmentDTO
{
    public class ApartmentSearchParamsDTO
    {
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public ApartmentSortDTO Sort { get; set; }
        public ApartmentFiltersDTO ApartmentFilters { get; set; }
    }
}