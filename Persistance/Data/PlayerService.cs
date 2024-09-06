using Application.Common.Interfaces;
using Application.Common.Models.Errors;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Persistance.Data
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OneOf<Unit, ClientError>> CreatePlayerAsync(Player player)
        {
            var result = await _context.Players.AddAsync(player);

            if (result.State != EntityState.Added)
            {
                return Errors.Internal.OperationFailed;
            }

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return Unit.Value;
            }

            return Errors.Internal.OperationFailed;
        }

        public async Task<OneOf<Unit, ClientError>> DeletePlayerAsync(Player player)
        {
            var result = _context.Players.Remove(player);

            if (result.State != EntityState.Deleted)
            {
                return Errors.Internal.OperationFailed;
            }

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return Unit.Value;
            }

            return Errors.Internal.OperationFailed;
        }

        public async Task<Player?> GetPlayerByEmailAsync(string email)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Player?> GetPlayerByNicknameAsync(string nickname)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Username == nickname);
        }

        public async Task<Player?> GetPlayerByUuidAsync(string uuid)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Uuid == uuid);
        }

        public async Task<OneOf<Unit, ClientError>> UpdatePlayerAsync(Player player)
        {
            var result = _context.Players.Update(player);

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return Unit.Value;
            }

            return Errors.Internal.OperationFailed;
        }
    }
}
