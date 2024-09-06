using Application.Common.Interfaces;
using Application.Common.Models.Errors;
using MediatR;
using OneOf;

namespace Application.User.Commands.UploadSkin
{
    public class UploadSkinCommandHandler
        : IRequestHandler<UploadSkinCommand, OneOf<Unit, ClientError>>
    {
        private readonly ISkinService _skinService;

        public UploadSkinCommandHandler(ISkinService skinService)
        {
            _skinService = skinService;
        }

        public async Task<OneOf<Unit, ClientError>> Handle(UploadSkinCommand request, CancellationToken cancellationToken)
        {
            OneOf<Unit, ClientError> result = await _skinService.UpdateSkinAsync(request.Username, request.File);

            return result;
        }
    }
}
