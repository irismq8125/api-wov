using api_wov.Models.Packets;
using api_wov.Models.ResponseOutput;

namespace api_wov.Models.Quests
{
    public class OutputQuest : InfoOutput
    {
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Url { get; set; }
        public int? RegistersGem { get; set; }
        public int? TotalGem { get; set; }
        public int? RegistersGold { get; set; }
        public int? TotalGold { get; set; }
        public List<OutputPacket> Packets { get; set; }
    }
}
