using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Services.ImageService
{
    public interface IUploadImagesService
    {
        Task<List<string>> UploadAsync(IFormCollection formFile, string username);
    }
}
