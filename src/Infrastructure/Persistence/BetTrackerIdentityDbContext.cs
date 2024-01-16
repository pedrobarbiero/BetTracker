using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence;

public class BetTrackerIdentityDbContext<AppUser>(DbContextOptions options) : IdentityDbContext(options)
{
}