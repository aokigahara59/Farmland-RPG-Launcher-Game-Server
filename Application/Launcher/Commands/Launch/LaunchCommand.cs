using Application.Common.Models;
using ErrorOr;
using MediatR;

namespace Application.Launcher.Commands.Launch
{
    public record LaunchCommand(
        string Username,
        string Email) : IRequest<ErrorOr<LaunchResponse>>;
}
