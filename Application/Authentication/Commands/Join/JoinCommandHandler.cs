using Application.Common.Interfaces;
using Application.Common.Models.Errors;
using MediatR;
using OneOf;

namespace Application.Authentication.Commands.Join
{
    public class JoinCommandHandler
        : IRequestHandler<JoinCommand, OneOf<Unit, ClientError>>
    {
        private readonly IPlayerService _playerService;

        public JoinCommandHandler(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<OneOf<Unit, ClientError>> Handle(JoinCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerService.GetPlayerByUuidAsync(request.Uuid);

            if (player is null 
                || player.AccessToken != request.AccessToken)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            player.ServerId = request.ServerId;

            OneOf<Unit, ClientError> result = await _playerService.UpdatePlayerAsync(player);

            return result;
        }
    }
}
