using Application.Contracts.Persistence;

namespace Persistence;

public class IdProvider : IIdProvider
{
    public Guid GetNextIdIfEmptyOrNull(Guid? currentId)
    {
        if (currentId == Guid.Empty || currentId == null)
            return Guid.NewGuid();

        return (Guid)currentId;
    }
}
