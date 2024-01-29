namespace api_wov.Models.RegisterQuests
{
    public class InputRegisterQuest
    {
        public string Name { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public List<Guid> Quests { get; set; }
        public int TotalGem { get; set; }
        public int TotalGold { get; set; }
        public int TotalMoney { get; set; }
        public int TotalExp { get; set; }
        public string Payments { get; set; }
    }
}
