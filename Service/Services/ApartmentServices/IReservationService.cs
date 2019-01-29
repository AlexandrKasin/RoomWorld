using System.Collections.Generic;
using System.Threading.Tasks;
using Service.DTO.apartmentReservation;
using Service.DTO.ApartmentDTO;

namespace Service.Services.ApartmentServices
{
    public interface IReservationService
    {
        Task AddReservationAsync(ReservationParamsDTO reservationParamsDTO);
        Task<IList<ApartmentReservationDTO>> GetReservationsByEmailAsync();
        Task<IList<ApartmentReservationDTO>> GetOrdersForUsersFlatsAsync();
    }
}