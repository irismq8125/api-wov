using api_wov.Models.Packets;
using api_wov.Models.ResponseOutput;

namespace api_wov.Models.QuestPackets
{
    public class OutputQuestPacket : InfoOutput
    {
        public List<OutputPacket> Packets { get; set; }
    }
}
