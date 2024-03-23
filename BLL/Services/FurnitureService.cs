using DataAccessLayer.Entities.MM;
using Microsoft.AspNetCore.Hosting;


namespace BLL.Services;

public class FurnitureService(IUnitOfWork unitOfWork,
                              IImageService imageService,
                              IWebHostEnvironment environment,
                              IDistributedCache cache)
    : IFurnitureService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly IImageService _imageService = imageService;
  private IWebHostEnvironment _environment = environment;
  private readonly IDistributedCache cache = cache;
  private const string _cacheKey = "furnitures";

  /// <summary>
  /// Create new furniture
  /// </summary>
  /// <param name="furnitureDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<FurnitureDto> CreateAsync(AddFurnitureDto furnitureDto,
                                             Language language)
  {
    if (furnitureDto is null)
    {
      throw new FurnitureException("Furniture was null");
    }

    var model = (Furniture)furnitureDto;
    if (!model.IsValidFurniture())
    {
      throw new FurnitureException("Furniture is not valid");
    }

    var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
    if (model.IsExist(furnitures))
    {
      throw new FurnitureException("Furniture already exists");
    }

    var colorIds = furnitureDto.ColorIds
                               .ToHashSet()
                               .ToList();
    var colors = await _unitOfWork.Colors.GetAllAsync();
    foreach (var colorId in colorIds)
    {
      var color = colors.FirstOrDefault(c => c.Id == colorId);
      if (color is null)
      {
        throw new FurnitureException("Color not found");
      }
      model.Colors!.Add(new FurnitureColor()
      {
        ColorId = colorId,
        Color = null,
        FurnitureId = model.Id,
        Furniture = null
      });
    }
    model.Category = null;

    model = _unitOfWork.Furnitures.Add(model);
    await _unitOfWork.SaveAsync();
    model = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(model.Id);

    foreach (var imageUrl in furnitureDto.ImageUrls)
    {
      var image = new Image
      {
        FurnitureId = model!.Id,
        Furniture = null,
        Url = imageUrl
      };
      _unitOfWork.Images.Add(image);
      await _unitOfWork.SaveAsync();
    }
    await cache.RemoveAsync(_cacheKey);
    return model!.ToDto(language);
  }

  /// <summary>
  /// Action with furniture (archive, unarchive, delete)
  /// </summary>
  /// <param name="id"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task ActionAsync(int id, ActionType action)
  {
    var furniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
    if (furniture is null)
    {
      throw new FurnitureException("Furniture not found");
    }

    switch (action)
    {
      case ActionType.Archive:
        furniture.IsActive = false;
        break;
      case ActionType.UnArchive:
        furniture.IsActive = true;
        break;
      case ActionType.Delete:
        var images = furniture.Images.ToList();
        foreach (var image in images)
        {
          string folder = _environment.WebRootPath;
          await _imageService.DeleteAsync(image.Url, folder);
        }
        _unitOfWork.Furnitures.Delete(id);
        await _unitOfWork.SaveAsync();
        return;
    }
    furniture.UpdatedAt = LocalTime.GetUtc5Time();
    _unitOfWork.Furnitures.Update(furniture);
    await _unitOfWork.SaveAsync();
    await cache.RemoveAsync(_cacheKey);
  }


  /// <summary>
  /// Get all furnitures with pagination
  /// </summary>
  /// <param name="pageSize"></param>
  /// <param name="pageNumber"></param>
  /// <returns></returns>
  public async Task<PagedList<FurnitureDto>> GetAllAsync(int pageSize, int pageNumber, Language language)
  {
    var furnitures = await _unitOfWork.Furnitures.GetAllAsyncWithDependencies();
    var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
    return new PagedList<FurnitureDto>(furnitureDtos, furnitureDtos.Count, pageNumber, pageSize);
  }

  /// <summary>
  /// Get all furnitures
  /// </summary>
  /// <returns></returns>
  public async Task<List<FurnitureDto>> GetAllAsync(Language language)
  {
    var furnitures = await _unitOfWork.Furnitures.GetAllAsyncWithDependencies();
    var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
    return furnitureDtos;
  }

  /// <summary>
  /// Get furniture by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<FurnitureDto> GetByIdAsync(int id, Language language)
  {
    var furniture = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(id);
    return furniture is null ?
        throw new FurnitureException("Furniture not found") : furniture.ToDto(language);
  }

  /// <summary>
  /// Update furniture
  /// </summary>
  /// <param name="furnitureDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<FurnitureDto> UpdateAsync(UpdateFurnitureDto furnitureDto,
                                             Language language)
  {
    if (furnitureDto is null)
    {
      throw new ArgumentNullException("Furniture was null");
    }

    var furniture = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(furnitureDto.Id);
    if (furniture is null)
    {
      throw new ArgumentNullException("Furniture not found");
    }

    var model = (Furniture)furnitureDto;
    if (!model.IsValidFurniture())
    {
      throw new FurnitureException("Furniture is not valid");
    }

    var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
    if (model.IsNotUnique(furnitures))
    {
      throw new FurnitureException("Furniture already exists");
    }

    var colorIds = furnitureDto.ColorIds
                               .ToHashSet()
                               .ToList();

    var colors = await _unitOfWork.Colors.GetAllAsync();
    var furnitureColors = _unitOfWork.FurnitureColors.GetAll(model.Id);
    if (!furnitureColors.All(fc => colorIds.Contains(fc.ColorId)))
    {
      _unitOfWork.FurnitureColors.DeleteRange(model.Id);
      await _unitOfWork.SaveAsync();

      var colorsForAdd = furnitureDto.ColorIds
                                     .ToHashSet()
                                     .ToList();
      foreach (var colorId in colorsForAdd)
      {
        _unitOfWork.FurnitureColors.Add(new FurnitureColor()
        {
          ColorId = colorId,
          Color = null,
          FurnitureId = model.Id,
          Furniture = null
        });
        await _unitOfWork.SaveAsync();
      }
    }

    model.Category = null;

    var imageDiffs = furniture.Images.Select(i => i.Url).Except(furnitureDto.ImageUrls);
    var images = await _unitOfWork.Images.GetAllAsync();
    foreach (var imageUrl in imageDiffs)
    {
      var image = images.FirstOrDefault(i => i.Url == imageUrl);
      _unitOfWork.Images.Delete(image!.Id);
      await _unitOfWork.SaveAsync();
    }

    _unitOfWork.Furnitures.Update(model);
    await _unitOfWork.SaveAsync();

    model = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(model.Id);

    if (model is null)
    {
      throw new FurnitureException("Furniture not found");
    }
    imageDiffs = furnitureDto.ImageUrls.Except(furniture.Images.Select(i => i.Url));
    foreach (var imageUrl in imageDiffs)
    {
      var image = new Image
      {
        FurnitureId = model.Id,
        Furniture = null,
        Url = imageUrl
      };
      _unitOfWork.Images.Add(image);
      await _unitOfWork.SaveAsync();
    }
    await cache.RemoveAsync(_cacheKey);
    return model.ToDto(language);
  }

  public async Task<SingleFurnitureDto> GetByIdAsync(int id)
  {
    var furniture = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(id);
    return furniture is null ?
        throw new FurnitureException("Furniture not found") : (SingleFurnitureDto)furniture;
  }
}