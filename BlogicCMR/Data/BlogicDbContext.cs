using Microsoft.EntityFrameworkCore;
using BlogicCMR.Models;

namespace BlogicCMR.Data;

public class BlogicDbContext : DbContext
{
    public BlogicDbContext(DbContextOptions<BlogicDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Advisor> Advisors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1: Client (1-N)
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Client)
            .WithMany(cl => cl.Contracts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2: Manager (1-N, bez nav zpět)
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Manager)
            .WithMany()
            .HasForeignKey(c => c.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 3: Participants (N-M s join tabulkou)
        modelBuilder.Entity<Contract>()
            .HasMany(c => c.Participants)
            .WithMany(a => a.Contracts)
            .UsingEntity(j => j.ToTable("ContractParticipants"));
    }

}