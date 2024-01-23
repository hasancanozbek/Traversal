using CloudinaryDotNet.Actions;
using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud
{
    public interface ICloudRepo
    {
        Task<string> UploadFileAsync(IFormFile file, FileTypesEnum type);
        bool DeleteFile(List<string> publicIdList);
        bool IsFileAvailable(string url, FileTypesEnum type);
        string GetFileUrl(string id);
        Task<GetResourceResult> GetFileWithUrl(string url);
        Task<bool> DeleteFileAsync(List<string> publicIdList);
        Task<bool> IsFileAvailableAsync(string url, FileTypesEnum type);
        Task<string> GetFileUrlAsync(string id);

    }
}
