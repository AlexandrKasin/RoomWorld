using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Services
{
    public interface IUploadImagesService
    {
        Task UploadAsync(IFormCollection formFile, string path);
    }
}
