using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.dto;

namespace Service.Services
{
    public class FlatService : IFlatService
    {
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public FlatService(IRepository<Flat> flatRepository, IMapper mapper,
            IRepository<User> userRepository)
        {
            _flatRepository = flatRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> AmountFlatByParamsAsync(SearchParams searchParams)
        {
            return (await _flatRepository.GetAllAsync(
                x => String.Equals(x.Location.Country, searchParams.Country, StringComparison.CurrentCultureIgnoreCase)
                     && String.Equals(x.Location.City, searchParams.City, StringComparison.CurrentCultureIgnoreCase)
                     && x.Orders.All(o =>
                         !(o.DateFrom.Date <= searchParams.DateFrom.Date && o.DateTo.Date >= searchParams.DateFrom.Date) &&
                         !(o.DateFrom.Date <= searchParams.DateTo.Date && o.DateTo.Date >= searchParams.DateTo.Date) &&
                         !(o.DateFrom.Date > searchParams.DateFrom.Date && o.DateFrom.Date < searchParams.DateTo.Date)), x => x.Location))
                .Count();
        }

        public async Task AddFlatAsync(Flat flat, string email)
        {
            var path = @"\images\uploaded\" + email + @"\";
            var imageCollection = new List<Image>(flat.Images);
            for (var i = 0; i < flat.Images.Count; i++)
            {
                imageCollection[i].Url = path + imageCollection[i].Url;
            }

            flat.Images = imageCollection;
            flat.CreatedBy = (await (await _userRepository.GetAllAsync(x => x.Email == email)).FirstOrDefaultAsync())
                .Id;
            flat.User = await (await _userRepository.GetAllAsync(x => x.Email == email)).FirstOrDefaultAsync();
            await _flatRepository.InsertAsync(flat);
        }

        public async Task<FlatViewModel> GetFlatByIdAsync(int id)
        {
            return _mapper.Map<FlatViewModel>(await (await _flatRepository.GetAllAsync(x => x.Id == id,
                    x => x.Location,
                    x => x.Amentieses,
                    x => x.HouseRuleses,
                    x => x.Images,
                    x => x.Orders))
                .FirstOrDefaultAsync());
        }

        public async Task<IList<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate,
            params Expression<Func<Flat, object>>[] includeParams)
        {
            return await (await _flatRepository.GetAllAsync(predicate, includeParams)).ToListAsync();
        }

        public async Task UpdateFlatAsync(Flat flat)
        {
            await _flatRepository.UpdateAsync(flat);
        }

        public async Task DeleteFlatAsync(Flat flat)
        {
            await _flatRepository.DeleteAsync(flat);
        }

        public async Task<IList<FlatViewModel>> SearchFlatAsync(SearchParams searchParams)
        {
            var flats = (await _flatRepository.GetAllAsync(
                    x => String.Equals(x.Location.Country, searchParams.Country, StringComparison.CurrentCultureIgnoreCase)
                         && String.Equals(x.Location.City, searchParams.City, StringComparison.CurrentCultureIgnoreCase)
                         && x.Orders.All(o =>
                             !(o.DateFrom.Date <= searchParams.DateFrom.Date && o.DateTo.Date >= searchParams.DateFrom.Date) && 
                             !(o.DateFrom.Date <= searchParams.DateTo.Date && o.DateTo.Date >= searchParams.DateTo.Date) && 
                             !(o.DateFrom.Date > searchParams.DateFrom.Date && o.DateFrom.Date < searchParams.DateTo.Date))
                    , x => x.Location, x => x.Amentieses, x => x.HouseRuleses, x => x.Images))
                .Skip(searchParams.Skip)
                .Take(searchParams.Take);
            return _mapper.Map<IList<FlatViewModel>>(flats);
        }
    }
}