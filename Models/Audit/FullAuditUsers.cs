namespace api_wov.Models.Audit
{
    public class FullAuditUsers : AuditUsers
    {
        public bool IsDeleted { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }

        public FullAuditUsers() 
        {
            this.IsDeleted = false;
            this.DeletionTime = null;
            this.DeleterId = null;
        }
    }
}
