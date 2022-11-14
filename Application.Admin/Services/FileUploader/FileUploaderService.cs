using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Admin.Services.FileUploader
{
    public class FileUploaderService : IFileUploaderService
    {
        private readonly string _basePath;
        private readonly ILogger _logger;

        public FileUploaderService(IWebHostEnvironment webHostEnvironment, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(FileUploaderService));
            _basePath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot");
        }

        public async Task<string> UploadAsync(IFormFile formFile, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Path is empty");

            path = Path.Combine(_basePath, Path.Combine(path.Split("/")));

            var fileExtension = Path.GetExtension(formFile.FileName);

            var fileName = $"{Guid.NewGuid()}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.ffffff")}{fileExtension}";

            var isUploaded = await SaveFileAsync(formFile, path, fileName);

            if (!isUploaded)
            {
                throw new Exception("There was an error while saving the file");
            }

            return fileName;
        }
        

        public bool Remove(string fileName)
        {
            try
            {
                fileName = Path.Combine(_basePath, Path.Combine(fileName.Split("/")));

                if (!File.Exists(fileName))
                    throw new Exception("File Not Found! " + fileName);

                File.Delete(fileName);
                return true;
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError($"При удаления файла произошла ошибка: {e.Message}, stack: {e.StackTrace}");
                throw;
            }
        }

        private async Task<bool> SaveFileAsync(IFormFile file, string path, string fileName)
        {
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (file == null || file.Length <= 0) return false;

                await using var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

                await file.CopyToAsync(fileStream);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"При сохранении файла произошла ошибка: {e.Message}, stack: {e.StackTrace}");
                return false;
            }
        }
    }
}