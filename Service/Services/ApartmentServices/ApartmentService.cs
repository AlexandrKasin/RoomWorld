using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity.ApartmentEntity;
using Data.Entity.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Repository.Repositories;
using Service.DTO.ApartmentDTO;
using Service.Exceptions;
using Service.Extension.ApartmentExtention;
using Service.Services.ImageService;

namespace Service.Services.ApartmentServices
{
    public class ApartmentService : IApartmentService
    {
        private readonly IRepository<Apartment> _apartmentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IUploadImagesService _imagesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;

        public ApartmentService(IRepository<Apartment> apartmentRepository, IMapper mapper,
            IRepository<User> userRepository, IUploadImagesService imagesService,
            IHttpContextAccessor httpContextAccessor, IRepository<ApartmentType> apartmentTypeRepository)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _imagesService = imagesService;
            _httpContextAccessor = httpContextAccessor;
            _apartmentTypeRepository = apartmentTypeRepository;
        }

        public async Task InsertApartmentAsync(ApartmentInsertDTO apartmentInsertDTO)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var owner = await (await _userRepository.GetAllAsync(x => x.Email.ToUpper().Equals(email.ToUpper())))
                .FirstOrDefaultAsync();
            var apartment = _mapper.Map<Apartment>(apartmentInsertDTO);
            var images = await _imagesService.UploadAsync(apartmentInsertDTO.Images, email);
            var apartmenrType =
                (await _apartmentTypeRepository.GetAllAsync(x =>
                    x.Name.ToUpper().Equals(apartmentInsertDTO.ApartmentTypeString.ToUpper())))
                .FirstOrDefault();
            apartment.ApartmentImages = images.Select(path => new ApartmentImage {Url = path}).ToList();
            apartment.ApartmentType =
                apartmenrType ?? throw new EntityNotExistException("Current apartment type not exists");
            apartment.Owner = owner;
            apartment.CreatedBy = owner.Id;
            /*apartment.ApartmentLocation.Coordinates = new Point(20.545, -80.4564){SRID = 4326};*/
            await _apartmentRepository.InsertAsync(apartment);
        }

        public async Task<List<string>> GetApartmentTypesAsync()
        {
            return await (await _apartmentTypeRepository.GetAllAsync()).Select(type => type.Name).ToListAsync();
        }

        public async Task<ApartmentDTO> GetApartmentByIdAsync(int id)
        {
            var apartment = await (await _apartmentRepository.GetAllAsync(x => x.Id == id, x => x.ApartmentLocation,
                    x => x.RulesOfResidence, x => x.ApartmentImages, x => x.ApartmentReservations,
                    x => x.ApartmentType))
                .FirstOrDefaultAsync();
            if (apartment == null) throw new EntityNotExistException("This apartment is not exists.");
            var apartmentDTO = _mapper.Map<ApartmentDTO>(apartment);
            return apartmentDTO;
        }

        public async Task<IList<ApartmentDTO>> GetApartmentByParamsAsync(ApartmentSearchParamsDTO searchParams)
        {
            var apartmentCollection =
                await (await _apartmentRepository.GetAllAsync(searchParams.GetExpression(),
                        x => x.ApartmentImages, x => x.ApartmentLocation, x => x.ApartmentType))
                    .Skip(searchParams.Skip).Take(searchParams.Take).ToListAsync();
            var apartmentCollectionDTO = _mapper.Map<IList<ApartmentDTO>>(apartmentCollection);
            return apartmentCollectionDTO;
        }

        public async Task<int> GetAmountApartmentByParamsAsync(ApartmentSearchParamsDTO searchParams)
        {
            var apartmentCollectionAmount =
                (await _apartmentRepository.GetAllAsync(searchParams.GetExpression())).Count();
            return apartmentCollectionAmount;
        }

        public async Task<IList<ApartmentDTO>> GetApartmentByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var apartmentCollection = await (await _apartmentRepository.GetAllAsync(
                x => string.Equals(x.Owner.Email.ToUpper(), email.ToUpper()),
                x => x.ApartmentReservations, x => x.ApartmentImages, x => x.ApartmentLocation)).ToListAsync();
            var apartmentCollectionDTO = _mapper.Map<IList<ApartmentDTO>>(apartmentCollection);
            return apartmentCollectionDTO;
        }
    }
}