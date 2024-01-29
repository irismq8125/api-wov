using api_wov.Models.Audit;

namespace api_wov.Models.Entities
{
    public class RegisterQuest : FullAuditUsers
    {
        public string Name { get; set; }
        public string Links { get; set; }
        public string Quests { get; set; }
        public string Totals { get; set; }
        public string Payments { get; set; }
    }

    public class Links
    {
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
    }

    public class Totals
    {
        public int Gold { get; set; }
        public int Gem { get; set; }
        public int Money { get; set; }
        public int Exp { get; set; }
    }

    public class Payments
    {
        public Guid PaymentId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
