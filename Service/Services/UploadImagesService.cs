using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Http;

namespace Service.Services
{
    public class UploadImagesService : IUploadImagesService
    {
        public async Task UploadAsync(IFormCollection formFile, string email)
        {
            var path = @".\images\uploaded\" + email;
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            foreach (var file in formFile.Files)
            {
                using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }
        }
    }
}