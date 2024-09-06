using Application.Common.Interfaces;
using Application.Common.Models.Errors;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace Infrastructure.Services
{
    public class SkinService : ISkinService
    {
        public readonly string _skinDirectory;
        public readonly string _baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SkinService(string skinDirectory, string baseUrl, IHttpContextAccessor httpContextAccessor)
        {
            _skinDirectory = skinDirectory;
            _httpContextAccessor = httpContextAccessor;

            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host}/{baseUrl}";
        }

        public string? GetSkinUrl(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return $"{_baseUrl}/default.png";
            }

            var userFiles = Directory.GetFiles(_skinDirectory, $"{username}*");

            if (userFiles.Length == 0)
            {
                return $"{_baseUrl}/default.png";
            }

            var filePath = userFiles[0];
            var fileName = Path.GetFileName(filePath);

            return $"{_baseUrl}/{fileName}";
        }

        public async Task<OneOf<Unit, ClientError>> UpdateSkinAsync(string username, Stream file)
        {
            Directory.CreateDirectory(_skinDirectory);

            var filesToDelete = Directory.GetFiles(_skinDirectory, $"*{username}*");

            foreach (var x in filesToDelete)
            {
                File.Delete(x);
            }

            var fileName = $"{username}_{Guid.NewGuid()}.png";

            using var fileStream = new FileStream(Path.Combine(_skinDirectory, fileName),
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                16384,
                useAsync: true);

            await file.CopyToAsync(fileStream);

            return Unit.Value;
        }
    }
}
