using api_wov.Models;
using api_wov.Services.RequestResults;

namespace api_wov.Services
{
    public interface ICrudService<TEntity, TEntityOutput, in Tkey>
    {
        Task<PagesResult<TEntityOutput>> WoVListAllAsync(RequestResult request);
        Task<TEntityOutput> WoVGetByIdAsync(Tkey id);
        Task<TEntityOutput> WoVAddAsync(TEntity entity);
        Task<TEntityOutput> WoVUpdateAsync(TEntityOutput entityOutput);
        Task<TEntityOutput> WoVDeleteAsync(Tkey id);
        TEntityOutput WoVMapToOutput(TEntity entity);
        TEntity WoVMapToEntity(TEntityOutput entityOutput);
    }
}
