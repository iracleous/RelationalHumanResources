using Microsoft.EntityFrameworkCore;
using RelationalHumanResources.Data;
using RelationalHumanResources.Dtos;

namespace RelationalHumanResources.Services
{
    public class GenService<T, K> : IGenService<T, K>
    {
        private readonly HrDbcontext _dbcontext;
        public GenService(HrDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<ApiResult<T>> CreateAsync(T model)
        {
            if (model != null)
            {
                _dbcontext.Add(model);
                await _dbcontext.SaveChangesAsync();
                return new ApiResult<T> { Data = model };
            }
            return new ApiResult<T> { 
                Status = -1,
                Message = "Empty input" 
            };
        }

        public Task<ApiResult<bool>> DeleteByIdAsync(K id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<T>>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<T>> ReadByIdAsync(K id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<T>> UpdateAsync(K id, T model)
        {
            throw new NotImplementedException();
        }
    }
}
