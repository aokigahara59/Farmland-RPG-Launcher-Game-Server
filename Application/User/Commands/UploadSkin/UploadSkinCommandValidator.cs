using FluentValidation;
using System.Drawing;

namespace Application.User.Commands.UploadSkin
{
    public class UploadSkinCommandValidator : AbstractValidator<UploadSkinCommand>
    {
        public UploadSkinCommandValidator()
        {
            RuleFor(x => x.File.Length).GreaterThan(0);

            RuleFor(x => x.File)
            .Must(stream =>
            {
                try
                {
                    using (var image = Image.FromStream(stream))
                    {
                        return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (stream.CanSeek)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                    }
                }
            })
            .WithMessage("File must be a valid PNG image.");

            RuleFor(x => x.File)
                .Must(stream =>
                {
                    try
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            return (image.Width == 64 && image.Height == 64) || (image.Width == 128 && image.Height == 128);
                        }
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        if (stream.CanSeek)
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                })
                .WithMessage("Skin must have a resolution of 64x64 or 128x128 pixels.");
        }
    }
}
