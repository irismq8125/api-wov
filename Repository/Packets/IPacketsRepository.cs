using api_wov.Services;

namespace api_wov.Repository.Packets
{
    public interface IPacketsRepository<TEntity, TEntityOutput, Tkey> : ICrudService<TEntity, TEntityOutput, Tkey>
    {
    }
}
