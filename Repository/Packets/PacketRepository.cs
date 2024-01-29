using api_wov.Models;
using api_wov.Models.Entities;
using api_wov.Models.Packets;
using api_wov.Services;
using api_wov.Services.RequestResults;
using Microsoft.EntityFrameworkCore;

namespace api_wov.Repository.Packets
{
    public class PacketRepository : IPacketsRepository<Packet, OutputPacket, Guid>
    {
        private readonly WovDbContext _context;
        public PacketRepository(WovDbContext context)
        {
            _context = context;
        }

        public async Task<OutputPacket> WoVAddAsync(Packet entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.Filters = Utilities.ConvertToUnsign(entity.Name);

            _context.Packets.Add(entity);
            await _context.SaveChangesAsync();
            return WoVMapToOutput(entity);
        }

        public async Task<OutputPacket> WoVDeleteAsync(Guid id)
        {
            var entity = await _context.Packets.FirstOrDefaultAsync(x => x.Id == id);
            entity.IsDeleted = true;
            entity.DeleterId = null;
            entity.DeletionTime = DateTime.UtcNow;
            _context.Packets.Update(entity);
            await _context.SaveChangesAsync();
            return WoVMapToOutput(entity);
        }

        public async Task<OutputPacket> WoVGetByIdAsync(Guid id)
        {
            var entity = await _context.Packets.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) throw new NotImplementedException();
            return WoVMapToOutput(entity);
        }

        public async Task<PagesResult<OutputPacket>> WoVListAllAsync(RequestResult request)
        {
            var items = await _context.Packets
                            .Where(c => c.Filters.ToLower().Contains(request.Filters.ToLower())
                                    & c.IsDeleted == false 
                                    & (request.Status == "" ? true : c.Status.Equals(request.Status)))
                            .OrderByDescending(x => x.CreationTime).ThenBy(x => x.Name)
                            .Skip(request.SkipCount > 0 ? request.SkipCount : 0)
                            .Take(request.MaxResultCount > 0 ? request.MaxResultCount : 10)
                            .Select(x => new OutputPacket
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Gold = x.Gold,
                                Gem = x.Gem,
                                Money = x.Money,
                                Exp = x.Exp,
                                CreationTime = x.CreationTime,
                                Status = x.Status,
                            })
                            .ToListAsync();
            PagesResult<OutputPacket> pagesResult = new PagesResult<OutputPacket>
            {
                TotalCount = items.Count,
                Items = items
            };
            return pagesResult;
        }

        public async Task<OutputPacket> WoVUpdateAsync(OutputPacket entity)
        {
            var item = WoVMapToEntity(entity);
            item.LastModificationTime = DateTime.UtcNow;
            item.Filters = Utilities.ConvertToUnsign(entity.Name);

            _context.Packets.Update(WoVMapToEntity(entity));
            await _context.SaveChangesAsync();
            return entity;
        }

        public OutputPacket WoVMapToOutput(Packet entity)
        {
            return new OutputPacket
            {
                Id = entity.Id,
                Name = entity.Name,
                Gold = entity.Gold,
                Gem = entity.Gem,
                Money = entity.Money,
                Exp = entity.Exp,
                CreationTime = entity.CreationTime,
                Status = entity.Status,
            };
        }

        public Packet WoVMapToEntity(OutputPacket entity)
        {
            var item = _context.Packets.FirstOrDefault(x => x.Id == entity.Id);
            item.Name = entity.Name;
            item.Gold = entity.Gold;
            item.Gem = entity.Gem;
            item.Money = entity.Money;
            item.Exp = entity.Exp;
            item.CreationTime = entity.CreationTime;
            item.Status = entity.Status;
            return item;
        }
    }
}
