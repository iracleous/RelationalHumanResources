using RelationalHumanResources.Dtos;

namespace RelationalHumanResources.Services
{
    public interface IGenService<T, K>
    {
        //CRUD
        public Task<ApiResult<T>> CreateAsync(T model);
        public Task<ApiResult<List<T>>> ReadAllAsync();
        public Task<ApiResult<T>> ReadByIdAsync(K id);
        public Task<ApiResult<T>> UpdateAsync(K id, T model);
        public Task<ApiResult<bool>> DeleteByIdAsync(K id);
    }


}
