using Application.Authentication.Commands.Join;
using Application.Authentication.Queries.HasJoin;
using Application.Launcher.Commands.Launch;
using Contracts.Common;
using Contracts.Game;
using Mapster;

namespace Presentation.Common.Mappings
{
    public class RequestMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HasJoinRequest, HasJoinedQuery>();

            config.NewConfig<JoinRequest, JoinCommand>()
                .Map(dest => dest.Uuid, src => src.SelectedProfile);

            config.NewConfig<LaunchRequest, LaunchCommand>();
        }
    }
}
