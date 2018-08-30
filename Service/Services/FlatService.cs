using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Repository.Repositories;

namespace Service.Services
{
    public class FlatService : IFlatService
    {
        private readonly IRepository<Flat> _repository;

        public FlatService(IRepository<Flat> repository)
        {
            _repository = repository;
        }


        public async Task AddFlatAsunc(Flat flat)
        {
            await _repository.InsertAsync(flat);
        }

        public async Task<Flat> GetFlatByIdAsunc(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IQueryable<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate)
        {
            return await _repository.GetAllAsync(predicate);
        }

        public async Task UpdateFlatAsunc(Flat flat)
        {
            await _repository.UpdateAsync(flat);
        }

        public async Task DeleteFlatAsunc(Flat flat)
        {
            await _repository.DeleteAsync(flat);
        }
    }
}