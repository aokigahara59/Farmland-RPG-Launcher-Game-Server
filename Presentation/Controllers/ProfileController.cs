using Application.Common.Models;
using Application.Launcher.Commands.Launch;
using Application.User.Commands.UploadSkin;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("edit-profile")]
    public class ProfileController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ProfileController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("launch")]
        public async Task<IActionResult> Launch()
        {
            var command = new LaunchCommand(User.FindFirstValue("nickname"),
                User.FindFirstValue(ClaimTypes.Email));

            ErrorOr<LaunchResponse> result = await _sender.Send(command);

            return result.Match<IActionResult>(Ok,BadRequest);
        }

        [HttpPut("change-skin")]
        public async Task<IActionResult> UploadSkin(IFormFile skin)
        {
            using var skinStream = skin.OpenReadStream();
            var command = new UploadSkinCommand(User.FindFirstValue("nickname"), skinStream);

            var result = await _sender.Send(command);

            return result.Match<IActionResult>(unit => Ok(), BadRequest);
        }
    }
}
