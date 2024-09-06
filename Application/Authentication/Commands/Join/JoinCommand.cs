using Application.Common.Models.Errors;
using MediatR;
using OneOf;

namespace Application.Authentication.Commands.Join
{
    public record JoinCommand(
        string AccessToken, 
        string Uuid,
        string ServerId) : IRequest<OneOf<Unit, ClientError>>;
}
