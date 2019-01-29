using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Services.ImageService
{
    public class UploadImagesService : IUploadImagesService
    {
        public async Task<List<string>> UploadAsync(IFormCollection formFile, string username)
        {
            var pathImages = new List<string>();
            var usersFolder = @"\images\uploaded\" + username;
            var dirInfo = new DirectoryInfo(usersFolder);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            foreach (var file in formFile.Files)
            {
                using (var fs = new FileStream(Path.Combine(@".\wwwroot" + usersFolder, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                    pathImages.Add(Path.Combine(usersFolder, file.FileName));
                }
            }

            return pathImages;
        }
    }
}