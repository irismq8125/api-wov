using api_wov.Models.Audit;
using System.ComponentModel.DataAnnotations;

namespace api_wov.Models.Entities
{
    public class Quest : FullAuditUsers
    {
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Url { get; set; }
        public int? RegistersGem { get; set; }
        public int? TotalGem { get; set; }
        public int? RegistersGold { get; set; }
        public int? TotalGold { get; set; }
    }
}
