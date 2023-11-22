using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class BetTrackerDbContext : DbContext
{
    public BetTrackerDbContext(DbContextOptions<BetTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BetTrackerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow; // Todo: use time provider
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            entry.Entity.UpdatedDate = utcNow;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Bet> Bets { get; set; }
}
