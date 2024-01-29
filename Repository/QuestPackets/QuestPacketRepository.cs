using api_wov.Models.Entities;
using api_wov.Models.Packets;
using api_wov.Models.QuestPackets;
using api_wov.Repository.Packets;
using Microsoft.EntityFrameworkCore;

namespace api_wov.Repository.QuestPackets
{
    public class QuestPacketRepository : IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid>
    {
        private readonly WovDbContext _context;
        public QuestPacketRepository(WovDbContext context)
        {
            _context = context;
        }

        public void AddAll(string questid)
        {
            try
            {
                var ids = _context.Packets.Where(x => x.IsDeleted == false).Select(x => x.Id).ToList();
                var temp = new List<QuestPacket>();
                foreach (var id in ids)
                {
                    var questpack = new QuestPacket();
                    questpack.Id = Guid.NewGuid();
                    questpack.PacketId = id;
                    questpack.QuestId = Guid.Parse(questid);
                    questpack.CreationTime = DateTime.UtcNow;

                    temp.Add(questpack);
                }
                _context.QuestPacket.AddRange(temp);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void AddQuestPacket(string questid, string packetId)
        {
            var questpack = new QuestPacket();
            questpack.PacketId = Guid.Parse(packetId);
            questpack.QuestId = Guid.Parse(questid);
            _context.QuestPacket.Add(questpack);
            _context.SaveChanges();
        }

        public async Task<OutputQuestPacket> GetPacketByQuestId(Guid QuestId)
        {
            var items = await _context.QuestPacket
                        .Where(x => x.QuestId == QuestId && x.IsDeleted == false)
                        .ToListAsync();
            var listPacket = new List<OutputPacket>();
            foreach (var item in items)
            {
                var packet = _context.Packets
                    .Where(x => x.Id == item.PacketId && x.IsDeleted == false)
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
                    }).ToList();
                if(packet.Count > 0)
                {
                    listPacket.AddRange(packet);
                }
                else
                {
                    _context.QuestPacket.Remove(item);
                    _context.SaveChanges();
                } 
            }
            return new OutputQuestPacket
            {
                Id = QuestId,
                CreationTime = items[0].CreationTime,
                Status = items[0].Status,
                Packets = listPacket
            };
        }
    }
}
