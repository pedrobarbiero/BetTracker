using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Models.Identity;
using Domain.Models;

namespace Persistence;

public class BetTrackerDbContext(DbContextOptions options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
        ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(BetTrackerDbContext).Assembly);
        var decimalProperties = builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
        foreach (var property in decimalProperties)
        {
            property.SetColumnType("decimal(18, 4)");
        }

        var stringProperties = builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null);
        foreach (var property in stringProperties)
        {
            property.SetMaxLength(256);
        }

        builder.Entity<ApplicationUser>().ToTable("ApplicationUsers", schema: "Identity");
        builder.Entity<ApplicationRole>().ToTable("ApplicationRoles", schema: "Identity");
        builder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaims", schema: "Identity");
        builder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRoles", schema: "Identity");
        builder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogins", schema: "Identity");
        builder.Entity<ApplicationRoleClaim>().ToTable("ApplicationRoleClaims", schema: "Identity");
        builder.Entity<ApplicationUserToken>().ToTable("ApplicationUserTokens", schema: "Identity");
    }

    public DbSet<Bankroll> Bankrolls { get; set; }
}