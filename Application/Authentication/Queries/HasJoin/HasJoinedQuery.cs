using MediatR;
using OneOf;

namespace Application.Authentication.Queries.HasJoin
{
    public record HasJoinedQuery(
        string Username, string ServerId) : IRequest<OneOf<object, Unit>>;
}
