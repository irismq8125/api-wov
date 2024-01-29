using api_wov.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace api_wov.Repository.Quests
{
    public interface IQuestsRepository<TEntity, TEntityOutput, Tkey> : ICrudService<TEntity, TEntityOutput, Tkey>
    {
    }
}
