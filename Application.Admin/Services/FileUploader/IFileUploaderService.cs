using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Admin.Services.FileUploader
{
    public interface IFileUploaderService
    {
        public Task<string> UploadAsync(IFormFile formFile, string path);
        

        public bool Remove(string filePath);
    }
}