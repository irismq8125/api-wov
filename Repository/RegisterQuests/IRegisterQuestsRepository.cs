using api_wov.Services;

namespace api_wov.Repository.RegisterQuests
{
    public interface IRegisterQuestsRepository<TEntity, TEntityOutput, Tkey> : ICrudService<TEntity, TEntityOutput, Tkey>
    {
    }
}
