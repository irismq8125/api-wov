using api_wov.Models.Audit;

namespace api_wov.Models.Entities
{
    public class QuestPacket : FullAuditUsers
    {
        public Guid QuestId { get; set; }
        public Guid PacketId { get; set; }
    }
}
