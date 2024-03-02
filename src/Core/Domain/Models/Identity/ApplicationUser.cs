using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Bankroll> Bankrolls { get; set; }
}