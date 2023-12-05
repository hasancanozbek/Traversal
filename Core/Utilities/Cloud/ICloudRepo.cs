using Core.Enums;

namespace Core.Utilities.Cloud
{
    public interface ICloudRepo
    {
        string UploadFile(string path, FileTypesEnum type);
        bool DeleteFile(string url, FileTypesEnum type);
        bool IsFileAvailable(string url, FileTypesEnum type);
        string GetFileUrl(string id);
        Task<string> UploadFileAsync(string path, FileTypesEnum type);
        Task<bool> DeleteFileAsync(string url, FileTypesEnum type);
        Task<bool> IsFileAvailableAsync(string url, FileTypesEnum type);
        Task<string> GetFileUrlAsync(string id);

    }
}
