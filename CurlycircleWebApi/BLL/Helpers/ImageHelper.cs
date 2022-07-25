using BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class ImageHelper
    {
        private readonly long _fileSizeLimit;

        public ImageHelper(IConfiguration configuration)
        {
            _fileSizeLimit = configuration.GetValue<long>("ImageSizeLimit");
        }

        public async Task<string> CreateThumbnailFile(IFormFile file, string savePath)
        {
            CheckFileExtension(file);
            CheckFileSize(file);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLowerInvariant();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), savePath, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                using Image image = Image.Load(memoryStream, out IImageFormat format);
                image.Mutate(x => x.Resize(width: 800, height: 0));
                using var outputStream = new FileStream(filePath, FileMode.Create);
                image.Save(outputStream, format);
            }

            return fileName;
        }

        public async Task<string> CreateImageFiles(IEnumerable<IFormFile> images, string savePath)
        {
            StringBuilder imageNames = new StringBuilder();

            foreach (var image in images)
            {
                CheckFileExtension(image);
                CheckFileSize(image);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName).ToLowerInvariant();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), savePath, fileName);

                using var outputStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(outputStream);

                imageNames.Append(fileName + ";");
            }

            return imageNames.ToString();
        }

        public void DeleteImageFile(string imageToDelete)
        {
            if (File.Exists(imageToDelete))
            {
                File.Delete(imageToDelete);
            }
        }

        private void CheckFileExtension(IFormFile file)
        {
            string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                throw new ValidationAppException("File upload failed.", new[]
                {
                    "The extension of the file is not accepted."
                });
            }
        }

        private void CheckFileSize(IFormFile file)
        {
            if (file.Length > _fileSizeLimit)
            {
                throw new ValidationAppException("File upload failed.", new[]
            {
                    "The size of the file is too big."
                });
            }
        }


    }
}
