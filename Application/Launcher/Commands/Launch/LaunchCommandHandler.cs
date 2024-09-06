using Application.Common.Interfaces;
using Domain.Models;
using Application.Common.Models;
using ErrorOr;

using MediatR;

namespace Application.Launcher.Commands.Launch
{
    public class LaunchCommandHandler
        : IRequestHandler<LaunchCommand, ErrorOr<LaunchResponse>>
    {
        private readonly IPlayerService _playerService;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IUuidGenerator _uuidGenerator;

        public LaunchCommandHandler(
            IPlayerService playerService,
            IAccessTokenGenerator accessTokenGenerator,
            IUuidGenerator uuidGenerator)
        {
            _playerService = playerService;
            _accessTokenGenerator = accessTokenGenerator;
            _uuidGenerator = uuidGenerator;
        }

        public async Task<ErrorOr<LaunchResponse>> Handle(LaunchCommand request, CancellationToken cancellationToken)
        {
            Player player = await _playerService.GetPlayerByEmailAsync(request.Email);

            if (player is not null)
            {
                player.Username = request.Username;
                player.AccessToken = _accessTokenGenerator.GenerateAccessToken();
                await _playerService.UpdatePlayerAsync(player);

                return new LaunchResponse(player.Username, player.AccessToken, player.Uuid);
            }

            player = new Player()
            {
                Username = request.Username,
                Uuid = _uuidGenerator.GenerateUuid(),
                Email = request.Email,
                AccessToken = _accessTokenGenerator.GenerateAccessToken(),
                ServerId = ""
            };

            await _playerService.CreatePlayerAsync(player);

            return new LaunchResponse(player.Username, player.AccessToken, player.Uuid);
        }
    }
}
