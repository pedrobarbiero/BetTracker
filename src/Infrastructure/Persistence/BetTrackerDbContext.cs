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
        var decimalProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
        foreach (var property in decimalProperties)
        {
            property.SetColumnType("decimal(10, 4)");
        }

        var stringProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null);
        foreach (var property in stringProperties)
        {
            property.SetMaxLength(256);
        }


        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = _timeProvider.GetUtcNow();
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
