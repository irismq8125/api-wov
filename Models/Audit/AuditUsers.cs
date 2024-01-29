using System.ComponentModel.DataAnnotations;

namespace api_wov.Models.Audit
{
    public class AuditUsers
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModificationTimeId { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.UtcNow;
        public Guid? CreationTimeId { get; set; }
        public string? Filters { get; set; } = string.Empty;
        public string? Status { get; set; } = "active";
    }
}
