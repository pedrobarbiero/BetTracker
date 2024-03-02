using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().HasData(new ApplicationUser { Id = Domain.Constants.Users.JokerId, UserName = "joker@bettracker.com", AccessFailedCount = 100 });

        builder.Entity<Sport>().HasData(
            new Sport() { Id = Domain.Constants.Sports.FootballId, Name = "Football", Slug = "football", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.CricketId, Name = "Cricket", Slug = "cricket", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.HockeyId, Name = "Hockey", Slug = "hockey", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.TennisId, Name = "Tennis", Slug = "tennis", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.VolleyballId, Name = "Volleyball", Slug = "volleyball", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.TableTennisId, Name = "Table Tennis", Slug = "table-tennis", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.BasketballId, Name = "Basketball", Slug = "basketball", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.BaseballId, Name = "Baseball", Slug = "baseball", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.RugbyId, Name = "Rugby", Slug = "rugby", ApplicationUserId = Domain.Constants.Users.JokerId },
            new Sport() { Id = Domain.Constants.Sports.GolfId, Name = "Golf", Slug = "golf", ApplicationUserId = Domain.Constants.Users.JokerId }
        );
    }

    public DbSet<Bankroll> Bankrolls { get; set; }
    public DbSet<Sport> Sports { get; set; }
}