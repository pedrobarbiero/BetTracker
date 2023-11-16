using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class BetTrackerDbContext : DbContext
{
    private readonly TimeProvider _timeProvider;
    public BetTrackerDbContext(DbContextOptions<BetTrackerDbContext> options, TimeProvider timeProvider) : base(options)
    {
        _timeProvider = timeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BetTrackerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
        {
            var utcNow = _timeProvider.GetUtcNow();
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
