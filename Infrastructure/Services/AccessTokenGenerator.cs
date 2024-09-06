using Application.Common.Interfaces;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        public string GenerateAccessToken()
        {
            var tokenData = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
}
