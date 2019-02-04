using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
                                    !(o.DateFrom.Date > searchParams.DateFrom.Date &&o.DateFrom.Date < searchParams.DateTo.Date))
                ;

        }
    }
}
