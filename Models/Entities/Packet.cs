using api_wov.Models.Audit;

namespace api_wov.Models.Entities
{
    public class Packet : FullAuditUsers
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Gem { get; set; }
        public int Money { get; set; }
        public int Exp { get; set; }
    }
}
