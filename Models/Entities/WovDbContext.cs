using Azure;
using Microsoft.EntityFrameworkCore;

namespace api_wov.Models.Entities
{
    public partial class WovDbContext : DbContext
    {
        public WovDbContext()
        {
        }

        public WovDbContext(DbContextOptions<WovDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Quest> Quests { get; set; }
        public virtual DbSet<Packet> Packets { get; set; }
        public virtual DbSet<QuestPacket> QuestPacket { get; set; }
        public virtual DbSet<RegisterQuest> RegisterQuests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Default");
    }
}
