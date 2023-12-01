namespace Application.Contracts.Persistence;

public interface IIdProvider
{
    Guid GetNextIdIfEmptyOrNull(Guid? currentId);
}
