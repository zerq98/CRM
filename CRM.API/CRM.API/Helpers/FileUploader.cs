﻿using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace CRM.API.Helpers
{
    public static class FileUploader
    {
        public static bool UploadFile(IFormFile file)
        {
            var folderName = Path.Combine("Resources", "ProfilePictures");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    file.CopyTo(stream);
                }

                return true;
            }

            return false;
        }
    }
}