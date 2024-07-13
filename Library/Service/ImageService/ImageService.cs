using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Image
{
    public class ImageService : IImageService
    {
        public async Task<string> UploadImage(IFormFile file, string type)
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "wwwroot", "Images", type);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(path, fileName);
                var stream = new FileStream(saveLocation, FileMode.Create);
                await file.CopyToAsync(stream);

                return fileName;
            }
            return null;
        }
    }
}
