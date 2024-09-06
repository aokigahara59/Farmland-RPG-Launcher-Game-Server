using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    public class UuidGenerator : IUuidGenerator
    {
        public string GenerateUuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
