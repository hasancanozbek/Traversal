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

        public bool DeleteFile(List<string> publicIdList)
        {
            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { "00000000-0000-0000-0000-000000000000" },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = cloudinary.DeleteResources(deleteParams);
            if (result.Deleted.Count() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteFileAsync(List<string> publicIdList)
        {
            var deleteParams = new DelResParams()
            {
                PublicIds = publicIdList,
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await cloudinary.DeleteResourcesAsync(deleteParams);
            if (result.Deleted.Count() != 0)
            {
                return true;
            }
            return false;
        }

        public string GetFileUrl(string id)
        {
            var result = cloudinary.GetResourceByAssetId(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Url;
            }
            return string.Empty;
        }

        public async Task<string> GetFileUrlAsync(string id)
        {
            var result = await cloudinary.GetResourceByAssetIdAsync(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Url;
            }
            return string.Empty;
        }

        public async Task<GetResourceResult> GetFileWithUrl(string url)
        {
            var publicId = string.Empty;
            if (url != null)
            {
                for (int i = url.Length - 1; i >= 0; i--)
                {
                    if (url[i] == '.')
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (url[j] == '/')
                            {
                                publicId = url.Substring(j + 1, i - j - 1);
                                var image = await cloudinary.GetResourceAsync(publicId);
                                return image;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public bool IsFileAvailable(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsFileAvailableAsync(string url, FileTypesEnum type)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile file, FileTypesEnum type)
        {
            switch (type)
            {
                case FileTypesEnum.Image:
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return uploadResult.AssetId;
                    }
                    return string.Empty;

                case FileTypesEnum.Video:
                    return string.Empty;

                case FileTypesEnum.Pdf:
                    return string.Empty;

                case FileTypesEnum.Excel:
                    return string.Empty;

                case FileTypesEnum.Text:
                    return string.Empty;

                default:
                    return string.Empty;
            }
        }
    }
}