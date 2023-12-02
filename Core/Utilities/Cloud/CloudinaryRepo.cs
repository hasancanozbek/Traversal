using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud
{
    public class CloudinaryRepo : ICloudRepo
    {
        private readonly Cloudinary cloudinary;
        public CloudinaryRepo()
        {
            Account account = new Account
            (
                "dr86l8ihb",
                "558868354686923",
                "xbVEJx4yeBE5KVw29KOx7WOzmAI"
            );
            cloudinary = new Cloudinary(account);
        }

        public bool DeleteFile(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFileAsync(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public string GetFileUrl(string id)
        {
            var result = cloudinary.GetResourceByAssetId(id);
            if (result.StatusCode.Equals("OK"))
            {
                return result.Url;
            }
            return string.Empty;
        }

        public async Task<string> GetFileUrlAsync(string id)
        {
            var result = await cloudinary.GetResourceByAssetIdAsync(id);
            if (result.StatusCode.Equals("OK"))
            {
                return result.Url;
            }
            return string.Empty;
        }

        public bool IsFileAvailable(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsFileAvailableAsync(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public bool UploadFile(string path, FileTypesEnum type)
        {
            switch (type)
            {
                case FileTypesEnum.Image:
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(path),
                        PublicId = new Guid().ToString()
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    if (uploadResult.StatusCode.Equals(StatusCodes.Status200OK))
                    {
                        return true;
                    }
                    return false;

                case FileTypesEnum.Video:
                    return false;

                case FileTypesEnum.Pdf:
                    return false;

                case FileTypesEnum.Excel:
                    return false;

                case FileTypesEnum.Text:
                    return false;

                default:
                    return false;
            }
        }

        public async Task<bool> UploadFileAsync(string path, FileTypesEnum type)
        {
            switch (type)
            {
                case FileTypesEnum.Image:
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(path),
                        PublicId = new Guid().ToString()
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    if (uploadResult.StatusCode.Equals("OK"))
                    {
                        return true;
                    }
                    return false;

                case FileTypesEnum.Video:
                    return false;

                case FileTypesEnum.Pdf:
                    return false;

                case FileTypesEnum.Excel:
                    return false;

                case FileTypesEnum.Text:
                    return false;

                default:
                    return false;
            }
        }
    }
}
