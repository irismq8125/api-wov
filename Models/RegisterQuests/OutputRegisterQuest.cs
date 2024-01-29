using api_wov.Models.Entities;
using api_wov.Models.ResponseOutput;

namespace api_wov.Models.RegisterQuests
{
    public class OutputRegisterQuest : InfoOutput
    {
        public string Name { get; set; }
        public Links Links { get; set; }
        public List<Guid> Quests { get; set; }
        public Totals Totals { get; set; }
        public Payments Payments { get; set; }
    }
}
