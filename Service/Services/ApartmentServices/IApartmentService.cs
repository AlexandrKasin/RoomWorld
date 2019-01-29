using System.Collections.Generic;
using System.Threading.Tasks;
using Service.DTO.ApartmentDTO;

namespace Service.Services.ApartmentServices
{
    public interface IApartmentService
    {
        Task InsertApartmentAsync(ApartmentInsertDTO apartmentInsertDTO);
        Task<List<string>> GetApartmentTypesAsync();
        Task<ApartmentDTO> GetApartmentByIdAsync(int id);
        Task<IList<ApartmentDTO>> GetApartmentByParamsAsync(ApartmentSearchParamsDTO searchParams);
        Task<int> GetAmountApartmentByParamsAsync(ApartmentSearchParamsDTO searchParams);
        Task<IList<ApartmentDTO>> GetApartmentByEmailAsync();
    }
}