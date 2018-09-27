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
        private readonly IRepository<Flat> _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FlatService(IRepository<Flat> repository, IUserService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }


        public async Task AddFlatAsunc(Flat flat, string email)
        {
            var path = @"\images\uploaded\" + email + @"\";
            var imageCollection = new List<Image>(flat.Images);
            for (var i = 0; i < flat.Images.Count; i++)
            {
                imageCollection[i].Url = path + imageCollection[i].Url;
            }

            flat.Images = imageCollection;
            flat.User = await (await _userService.GetAllAsync(x => x.Email == email)).FirstOrDefaultAsync();
            await _repository.InsertAsync(flat);
        }

        public async Task<FlatViewModel> GetFlatByIdAsunc(int id)
        {
            return _mapper.Map<FlatViewModel>(await (await GetAllAsync(x => x.Id == id,
                    x => x.Location,
                    x => x.Amentieses,
                    x => x.Extrases,
                    x => x.HouseRuleses,
                    x => x.Images,
                    x => x.Orders))
                .FirstOrDefaultAsync());
        }

        public async Task<IQueryable<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate,
            params Expression<Func<Flat, object>>[] includeParams)
        {
            return await _repository.GetAllAsync(predicate, includeParams);
        }

        public async Task UpdateFlatAsunc(Flat flat)
        {
            await _repository.UpdateAsync(flat);
        }

        public async Task DeleteFlatAsunc(Flat flat)
        {
            await _repository.DeleteAsync(flat);
        }

        public async Task<ICollection<FlatViewModel>> SearchFlatAsunc(SearchParams searchParams)
        {
            var flats = (await GetAllAsync(
                    x => x.Location.Country.ToLower() == searchParams.Country.ToLower()
                         && x.Location.City.ToLower() == searchParams.City.ToLower()
                         && x.Orders.All(o =>
                             !(o.DateFrom.Date <= searchParams.DateFrom.Date &&
                               o.DateTo.Date >= searchParams.DateFrom.Date)
                             && !(o.DateFrom.Date <= searchParams.DateTo.Date &&
                                  o.DateTo.Date >= searchParams.DateTo.Date)
                             && !(o.DateFrom.Date > searchParams.DateFrom.Date &&
                                  o.DateFrom.Date < searchParams.DateTo.Date))
                    , x => x.Location, x => x.Amentieses, x => x.Extrases, x => x.HouseRuleses, x => x.Images))
                .Skip(searchParams.Skip)
                .Take(searchParams.Take);
            return _mapper.Map<ICollection<FlatViewModel>>(flats);
        }
    }
}