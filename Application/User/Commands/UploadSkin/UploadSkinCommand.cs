using Application.Common.Models.Errors;
using MediatR;
using OneOf;

namespace Application.User.Commands.UploadSkin
{
    public record UploadSkinCommand(
        string Username,
        Stream File) : IRequest<OneOf<Unit, ClientError>>;
}
