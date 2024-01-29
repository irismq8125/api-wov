using api_wov.Models;
using api_wov.Models.Entities;
using api_wov.Models.Quests;
using api_wov.Services;
using api_wov.Services.RequestResults;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Filters;
using System;
using System.Xml;
using api_wov.Models.Packets;

namespace api_wov.Repository.Quests
{
    public class QuestRepository : IQuestsRepository<Quest, OutputQuest, Guid>
    {
        private readonly WovDbContext _context;
        public QuestRepository(WovDbContext context)
        {
            _context = context;
        }

        public async Task<OutputQuest> WoVAddAsync(Quest entity)
        {
            entity.RegistersGem = 0;
            entity.TotalGem = 0;
            entity.RegistersGold = 0;
            entity.TotalGold = 0;
            entity.CreationTime = DateTime.Now;


            _context.Quests.Add(entity);
            await _context.SaveChangesAsync();
            return WoVMapToOutput(entity);
        }

        public async Task<OutputQuest> WoVDeleteAsync(Guid id)
        {
            var entity = await _context.Quests.FirstOrDefaultAsync(x => x.Id == id);
            entity.DeleterId = null;
            entity.DeletionTime = DateTime.UtcNow;
            entity.IsDeleted = true;

            _context.Quests.Update(entity);
            await _context.SaveChangesAsync();
            return WoVMapToOutput(entity); ;
        }

        public async Task<OutputQuest> WoVGetByIdAsync(Guid id)
        {
            var item = await _context.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null)
            {
                throw new NotImplementedException();
            }
            return WoVMapToOutput(item);
        }

        public async Task<PagesResult<OutputQuest>> WoVListAllAsync(RequestResult request)
        {
            var items = await _context.Quests
                .Where(x => x.Filters.ToLower().Contains(request.Filters.ToLower())
                        & x.IsDeleted == false
                        & (request.Status == "" ? true : x.Status.Equals(request.Status)))
                .OrderBy(x => x.Name).ThenByDescending(x => x.CreationTime)
                .Skip(request.SkipCount > 0 ? request.SkipCount : 0)
                .Take(request.MaxResultCount > 0 ? request.MaxResultCount : 10)
                .Select(x => new OutputQuest
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Url = x.Url,
                    RegistersGem = x.RegistersGem,
                    TotalGem = x.TotalGem,
                    RegistersGold = x.RegistersGold,
                    TotalGold = x.TotalGold,
                    CreationTime = x.CreationTime,
                    Status = x.Status
                })
                .ToListAsync();
            
            foreach (var item in items)
            {
                var listpackets = new List<OutputPacket>();
                var questPackets = _context.QuestPacket.Where(x => x.QuestId == item.Id).ToList();
                foreach (var questPacket in questPackets)
                {
                    var packet = _context.Packets
                                    .Where(x => x.Id == questPacket.PacketId)
                                    .Select(x => new OutputPacket
                                    {
                                        Id= x.Id,
                                        Name = x.Name,
                                        Gold = x.Gold,
                                        Gem = x.Gem,
                                        Money = x.Money,
                                        Exp = x.Exp,
                                        CreationTime= x.CreationTime,
                                        Status = x.Status
                                    })
                                    .ToList();
                    listpackets.AddRange(packet);
                }
                item.Packets = listpackets;
            }
            PagesResult<OutputQuest> pagesResult = new PagesResult<OutputQuest>
            {
                TotalCount = items.Count,
                Items = items,
            };
            return pagesResult;
        }

        public async Task<OutputQuest> WoVUpdateAsync(OutputQuest entity)
        {
            var item = WoVMapToEntity(entity);
            item.LastModificationTime = DateTime.UtcNow;
            item.Filters = Utilities.ConvertToUnsign(entity.Name);

            _context.Quests.Update(item);
            await _context.SaveChangesAsync();
            return entity;
        }

        public OutputQuest WoVMapToOutput(Quest entity)
        {
            return new OutputQuest
            {
                Id = entity.Id,
                Name = entity.Name,
                ShortDescription = entity.ShortDescription,
                Url = entity.Url,
                RegistersGem = entity.RegistersGem,
                TotalGem = entity.TotalGem,
                RegistersGold = entity.RegistersGold,
                TotalGold = entity.TotalGold,
                CreationTime = entity.CreationTime,
                Status = entity.Status,
            };
        }

        public Quest WoVMapToEntity(OutputQuest entity)
        {
            var item = _context.Quests.FirstOrDefault(x => x.Id == entity.Id);
            item.Name = entity.Name;
            item.ShortDescription = entity.ShortDescription;
            item.Url = entity.Url;
            item.RegistersGem = entity.RegistersGem;
            item.TotalGem = entity.TotalGem;
            item.RegistersGold = entity.RegistersGold;
            item.TotalGold = entity.TotalGold;
            item.CreationTime = entity.CreationTime;
            item.Status = entity.Status;
            return item;
        }
    }
}
