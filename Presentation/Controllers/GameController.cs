using Application.Authentication.Commands.Join;
using Application.Authentication.Queries.GetProfile;
using Application.Authentication.Queries.HasJoin;
using Application.Common.Models.Errors;
using Contracts.Game;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("minecraft")]
    public class GameController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public GameController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("hasJoined")]
        public async Task<IActionResult> HasJoined([FromQuery] HasJoinRequest request)
        {
            var query = _mapper.Map<HasJoinedQuery>(request);
            OneOf<object, Unit> result = await _sender.Send(query);

            return result.Match<IActionResult>(
                 Ok,
                 error => NotFound());
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody] JoinRequest request)
        {      
            var command = _mapper.Map<JoinCommand>(request);
            OneOf<Unit, ClientError> result = await _sender.Send(command);

            return result.Match<IActionResult>(
                 unit => Ok(),
                 BadRequest);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile(string uuid)
            {
            var query = new GetProfileQuery(uuid);
            OneOf<object, Unit> result = await _sender.Send(query);

            return result.Match<IActionResult>(
                 Ok,
                 error => NotFound());
        }
    }
}
