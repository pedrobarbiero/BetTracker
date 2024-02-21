using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Models.Identity;

namespace Persistence;

public class BetTrackerIdentityDbContext(DbContextOptions options) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users", schema: "Identity");
        builder.Entity<Role>().ToTable("Roles", schema: "Identity");
        builder.Entity<UserClaim>().ToTable("UserClaims", schema: "Identity");
        builder.Entity<UserRole>().ToTable("UserRoles", schema: "Identity");
        builder.Entity<UserLogin>().ToTable("UserLogins", schema: "Identity");
        builder.Entity<RoleClaim>().ToTable("RoleClaims", schema: "Identity");
        builder.Entity<UserToken>().ToTable("UserTokens", schema: "Identity");
    }
}