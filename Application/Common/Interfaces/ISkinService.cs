using Application.Common.Models.Errors;
using MediatR;
using OneOf;

namespace Application.Common.Interfaces
{
    public interface ISkinService
    {
        string? GetSkinUrl(string username);
        Task<OneOf<Unit, ClientError>> UpdateSkinAsync(string email, Stream file);
    }
}
