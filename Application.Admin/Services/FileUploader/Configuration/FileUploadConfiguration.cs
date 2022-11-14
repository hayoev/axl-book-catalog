namespace Application.Admin.Services.FileUploader.Configuration
{
    public class FileUploadConfiguration
    {
        public const string Key = "FileUpload";

        public string Folder { get; set; }
        public string HostAddress { get; set; }
    }
}