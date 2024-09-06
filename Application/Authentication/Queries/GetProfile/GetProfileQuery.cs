using MediatR;
using OneOf;

namespace Application.Authentication.Queries.GetProfile
{
    public record GetProfileQuery(string Uuid) : IRequest<OneOf<object, Unit>>;
}
