namespace api_wov.Models.Packets
{
    public class InputPacket
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Gem { get; set; }
        public int Money { get; set; }
        public int Exp { get; set; }
        public string? Status { get; set; }
    }
}
