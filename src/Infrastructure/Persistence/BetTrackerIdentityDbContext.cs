using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Models.Identity;

namespace Persistence;

public class BetTrackerIdentityDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
        //builder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
        //builder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaims");
        //builder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRoles");
        //builder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogins");
        //builder.Entity<ApplicationRoleClaim>().ToTable("ApplicationRoleClaims");
        //builder.Entity<ApplicationUserToken>().ToTable("ApplicationUserTokens");
    }
}