﻿using BLL.Interfaces;

namespace BLL.Services;

public class ImageService : IImageService
{
  /// <summary>
  /// Delete image from server by url 
  /// </summary>
  /// <param name="url"></param>
  /// <param name="folder"></param>
  /// <returns></returns>
  public Task DeleteAsync(string url, string folder)
  {
    string file = string.Empty;
    if (url.Contains('/'))
    {
      file = url.Split('/')[^1];
    }
    else
    {
      file = url.Split("%2F")[^1];
    }

    var filePath = Path.Combine(folder, "uploads", file);
    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }
    else
    {
      throw new FileNotFoundException("File not found", filePath);
    }
    return Task.CompletedTask;
  }

  public async Task DeleteAsync(List<string> urls, string folder)
  {
    foreach (var url in urls)
    {
      await DeleteAsync(url, folder);
    }
  }

  /// <summary>
  /// Upload image to server and return url
  /// </summary>
  /// <param name="file"></param>
  /// <param name="folder"></param>
  /// <param name="domain"></param>
  /// <returns></returns>
  public async Task<string> UploadAsync(IFormFile file, string folder, string domain)
  {
    if (file is null)
    {
      throw new ArgumentNullException(nameof(file));
    }

    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    string filePath = Path.Combine(folder, "uploads", fileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(fileStream);
    }

    string url = $"{domain}/uploads/{fileName}";

    return url;
  }

  public async Task<IEnumerable<string>> UploadAsync(List<IFormFile> files, string folder, string domain)
  {
    List<string> result = new();
    foreach (var file in files)
    {
      result.Add(await UploadAsync(file, folder, domain));
    }
    return result;
  }
}