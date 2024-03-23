namespace BLL.Interfaces;

public interface IImageService
{
  Task<string> UploadAsync(IFormFile file, string folder, string domain);

  Task<IEnumerable<string>> UploadAsync(List<IFormFile> files, string folder, string domain);

  Task DeleteAsync(string url, string folder);

  Task DeleteAsync(List<string> urls, string folder);
}