using Domain.Models.Identity;

namespace Domain.Common;

public class BaseEntityUser: BaseEntity
{
    public required Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }
}
