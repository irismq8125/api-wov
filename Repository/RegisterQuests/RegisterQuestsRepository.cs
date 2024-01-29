using api_wov.Models;
using api_wov.Models.Entities;
using api_wov.Models.RegisterQuests;
using api_wov.Services;
using api_wov.Services.RequestResults;
using Newtonsoft.Json;

namespace api_wov.Repository.RegisterQuests
{
    public class RegisterQuestsRepository : IRegisterQuestsRepository<RegisterQuest, OutputRegisterQuest, Guid>
    {
        private readonly WovDbContext _context;
        public RegisterQuestsRepository(WovDbContext context) 
        {
            _context = context;
        }

        public async Task<OutputRegisterQuest> WoVAddAsync(RegisterQuest entity)
        {
            entity.CreationTime = DateTime.UtcNow;
            entity.Filters = Utilities.ConvertToUnsign(entity.Name.ToLower());
            entity.Status = Utilities.Status.Active;
            _context.Add(entity);
            await  _context.SaveChangesAsync();
            return WoVMapToOutput(entity);
        }

        public Task<OutputRegisterQuest> WoVDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OutputRegisterQuest> WoVGetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagesResult<OutputRegisterQuest>> WoVListAllAsync(RequestResult request) 
        {
            var items = _context.RegisterQuests
                .Where(x => x.IsDeleted == false && (request.Status == "" ? true : x.Status.Equals(request.Status)))
                .OrderBy(x => x.CreationTime).ThenBy(x => x.Name)
                .Skip(request.SkipCount > 0 ? request.SkipCount : 0)
                .Take(request.MaxResultCount > 0 ? request.MaxResultCount : 10)
                .Select(WoVMapToOutput)
                .ToList();

            return new PagesResult<OutputRegisterQuest> 
            { 
                TotalCount = items.Count,
                Items = items 
            };
        }

        public RegisterQuest WoVMapToEntity(OutputRegisterQuest entityOutput)
        {
            throw new NotImplementedException();
        }

        public OutputRegisterQuest WoVMapToOutput(RegisterQuest entity)
        {
            var links = JsonConvert.DeserializeObject<Links>(entity.Links);
            var quests = JsonConvert.DeserializeObject<List<Guid>>(entity.Quests);
            var totals = JsonConvert.DeserializeObject<Totals>(entity.Totals);
            var payments = JsonConvert.DeserializeObject<Payments>(entity.Payments);
            return new OutputRegisterQuest
            {
                
                Id = entity.Id,
                Name = entity.Name,
                Links = new Links { Facebook = links.Facebook, Instagram = links.Instagram },
                Quests = quests,
                Totals = totals,
                Payments = payments,
                CreationTime = entity.CreationTime,
                Status = entity.Status,
            };
        }

        public Task<OutputRegisterQuest> WoVUpdateAsync(OutputRegisterQuest entityOutput)
        {
            throw new NotImplementedException();
        }
    }
}
