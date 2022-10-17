using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public interface IImageHelper
    {
        Task<string> CreateImageFiles(IEnumerable<IFormFile> images, string savePath);
        Task<string> CreateThumbnailFile(IFormFile file, string savePath);
        void DeleteImageFile(string imageToDelete);
    }
}