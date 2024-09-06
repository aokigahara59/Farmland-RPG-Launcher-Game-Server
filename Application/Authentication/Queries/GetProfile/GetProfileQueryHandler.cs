using Application.Common.Interfaces;
using MediatR;
using OneOf;
using System.Text;
using Newtonsoft.Json;

namespace Application.Authentication.Queries.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, OneOf<object, Unit>>
    {
        private readonly IPlayerService _playerService;
        private readonly ISkinService _skinService;

        public GetProfileQueryHandler(
            IPlayerService playerService, 
            ISkinService skinService)
        {
            _playerService = playerService;
            _skinService = skinService;
        }

        public async Task<OneOf<object, Unit>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _playerService.GetPlayerByUuidAsync(request.Uuid);

            if (user is null)
            {
                return Unit.Value;
            }

            // external API requeires anonimus type
            var texturesData = new
            {
                timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                profileId = user.Uuid,
                profileName = user.Username,
                textures = new
                {
                    SKIN = new
                    {
                        url = _skinService.GetSkinUrl(user.Username)
                    }
                }
            };

            string textureBase64 = Convert
                .ToBase64String(
                Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(texturesData)));

            return new
            {
                id = user.Uuid,
                name = user.Username,
                properties = new List<object>
                {
                    new
                    {
                        name = "textures",
                        value = textureBase64,
                        signature = "ignore"
                    }
                }
            };
        }
    }
}
