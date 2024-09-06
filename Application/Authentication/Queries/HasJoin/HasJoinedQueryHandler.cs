using Application.Authentication.Queries.GetProfile;
using Application.Common.Interfaces;
using MediatR;
using OneOf;

namespace Application.Authentication.Queries.HasJoin
{
    public class HasJoinedQueryHandler
        : IRequestHandler<HasJoinedQuery, OneOf<object, Unit>>
    {
        private readonly IPlayerService _playerService;
        private readonly ISender _sender;

        public HasJoinedQueryHandler(IPlayerService playerService, ISender sender)
        {
            _playerService = playerService;
            _sender = sender;
        }

        public async Task<OneOf<object, Unit>> Handle(HasJoinedQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerService.GetPlayerByNicknameAsync(request.Username);

            if (player is null || player.ServerId != request.ServerId)
            {
                return Unit.Value;
            }

            var query = new GetProfileQuery(player.Uuid);
            OneOf<object, Unit> result = await _sender.Send(query, cancellationToken);

            return result;
        }
    }
}
