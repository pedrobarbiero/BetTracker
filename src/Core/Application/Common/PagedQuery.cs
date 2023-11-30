using MediatR;

namespace Application.Common;

public record PagedQuery<TResponse> : IRequest<PagedResult<TResponse>>
{
    const uint maxPageSize = 100;
    public uint Page { get; set; } = 1;
    private uint _pageSize = 10;

    public uint PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }
}


