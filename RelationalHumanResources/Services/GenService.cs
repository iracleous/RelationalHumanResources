using Microsoft.EntityFrameworkCore;
using RelationalHumanResources.Data;
using RelationalHumanResources.Dtos;

namespace RelationalHumanResources.Services
{
    public class GenService<T, K> : IGenService<T, K> where T : class
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

        public async Task<ApiResult<bool>> DeleteByIdAsync(K id)
        {
            T? item = await _dbcontext.Set<T>().FindAsync(id);
            if (item != null)
            {
                _dbcontext.Set<T>().Remove(item);
                await _dbcontext.SaveChangesAsync();
                return new ApiResult<bool> { Data = true };
            }
            else
            { 
                return new ApiResult<bool> {  Status = -1 }; 
            }
        }

        public async Task<ApiResult<List<T>>> ReadAllAsync()
        {
            return  new ApiResult<List<T>> { Data = await _dbcontext.Set<T>().ToListAsync() };
        }

        public async Task<ApiResult<T>> ReadByIdAsync(K id)
        {
            return new ApiResult<T> { 
                Data = await _dbcontext
                .Set<T>()
                .FindAsync (id)  
            };
        }

        public Task<ApiResult<T>> UpdateAsync(K id, T model)
        {
            throw new NotImplementedException();
        }
    }
}
