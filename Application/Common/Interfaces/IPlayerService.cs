using Application.Common.Models.Errors;
using Domain.Models;
using MediatR;
using OneOf;

namespace Application.Common.Interfaces
{
    public interface IPlayerService
    {
        Task<Player?> GetPlayerByNicknameAsync(string nickname);
        Task<Player?> GetPlayerByEmailAsync(string email);
        Task<Player?> GetPlayerByUuidAsync(string uuid);
        Task<OneOf<Unit, ClientError>> CreatePlayerAsync(Player player);
        Task<OneOf<Unit, ClientError>> UpdatePlayerAsync(Player player);
        Task<OneOf<Unit, ClientError>> DeletePlayerAsync(Player player);
    }
}
