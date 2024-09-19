using Microsoft.Extensions.FileProviders;

namespace Clever.Web.Services;

public class ImageManager
{
    public async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        var fileName = Path.GetFileName(imageFile.FileName);
        var newFileName = String.Concat(Guid.NewGuid(), Path.GetExtension(fileName));
        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")).Root + $"{newFileName}";
        using FileStream fs = File.Create(filepath);
        await imageFile.CopyToAsync(fs);
        fs.Flush();
        return $"/images/{newFileName}";
    }
}