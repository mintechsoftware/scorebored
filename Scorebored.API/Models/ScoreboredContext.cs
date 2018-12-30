using Microsoft.EntityFrameworkCore;

namespace Scorebored.API.Models
{
    public class ScoreboredContext : DbContext
    {
        public ScoreboredContext(DbContextOptions<ScoreboredContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<GroupInvitation> GroupInvitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMembership>().HasKey(t => new { t.GroupId, t.UserId });
            modelBuilder.Entity<GroupInvitation>().HasKey(t => new { t.Code });
        }
    }
}
