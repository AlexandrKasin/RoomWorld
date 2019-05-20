using System;
using System.Linq;
using System.Linq.Expressions;
using Data.Entity.ApartmentEntity;
using Service.DTO.ApartmentDTO;

namespace Service.Extension.ApartmentExtention
{
    public static class ApartmentSearchParamsExt
    {
        public static Expression<Func<Apartment, bool>> GetExpression(this ApartmentSearchParamsDTO searchParams)
        {
            return apartment => (string.IsNullOrWhiteSpace(searchParams.Country) || string.Equals(apartment.ApartmentLocation.Country.ToUpper(),searchParams.Country.ToUpper())) &&
                                (string.IsNullOrWhiteSpace(searchParams.City) || string.Equals(apartment.ApartmentLocation.City.ToUpper(),searchParams.City.ToUpper())) &&
                                apartment.ApartmentReservations.All(o =>
                                    !(o.DateFrom.Date <= searchParams.DateFrom.Date && o.DateTo.Date >= searchParams.DateFrom.Date) &&
                                    !(o.DateFrom.Date <= searchParams.DateTo.Date && o.DateTo.Date >= searchParams.DateTo.Date) &&
                                    !(o.DateFrom.Date > searchParams.DateFrom.Date &&o.DateFrom.Date < searchParams.DateTo.Date)) &&
                                (searchParams.ApartmentFilters == null ||
                                (!searchParams.ApartmentFilters.AmountBathrooms.HasValue || apartment.AmountBathroom >= searchParams.ApartmentFilters.AmountBathrooms) &&
                                (!searchParams.ApartmentFilters.AmountBedrooms.HasValue || apartment.AmountBedroom >= searchParams.ApartmentFilters.AmountBedrooms) &&
                                (!searchParams.ApartmentFilters.MinCost.HasValue || apartment.ApartmentRates >= searchParams.ApartmentFilters.MinCost) && 
                                (!searchParams.ApartmentFilters.MaxCost.HasValue || apartment.ApartmentRates <= searchParams.ApartmentFilters.MaxCost));
        }

        public static Expression<Func<Apartment, double>> GetSortExpression(this ApartmentSortDTO apartmentSort)
        {
            switch(apartmentSort.Type.ToLower())
            {
                case "price":
                {
                    return apartment => apartment.ApartmentRates;                  
                }
                /*case "rating":
                {
                    return apartment => apartment.;
                }*/
                case "bedrooms":
                {
                    return apartment => apartment.AmountBedroom;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
