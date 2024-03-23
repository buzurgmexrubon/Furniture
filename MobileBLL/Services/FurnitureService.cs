namespace MobileBLL.Services;

public class FurnitureService(IUnitOfWork unitOfWork,
                              IDistributedCache cache)
    : IFurnitureService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly ICacheService<Furniture> _cacheService =
      new CacheService<Furniture>(cache);
  private const string _cacheKey = "furnitures";

  public async Task<List<FurnitureDto>> GetFurnituresAsync(Language language)
  {
    var furnitures = await _cacheService.GetFromCacheAsync(_cacheKey);
    if (furnitures == null)
    {
      furnitures = await _unitOfWork.Furnitures.GetAllAsyncWithDependencies();
      await _cacheService.SaveToCacheAsync(JsonConvert.SerializeObject(furnitures, Formatting.None,
                                          new JsonSerializerSettings()
                                          {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                          }), _cacheKey);
    }

    var list = furnitures.Select(f => f.ToDto(language)).ToList();
    return list;
  }

  public async Task<SingleFurnitureDto> GetSingleFurnitureAsync(int id, Language language)
  {
    var furnitures = await _cacheService.GetFromCacheAsync(_cacheKey);
    if (furnitures == null)
    {
      furnitures = await _unitOfWork.Furnitures.GetAllAsyncWithDependencies();
      await _cacheService.SaveToCacheAsync(JsonConvert.SerializeObject(furnitures, Formatting.None,
                                          new JsonSerializerSettings()
                                          {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                          }), _cacheKey);
    }

    var furnitureDto = furnitures.FirstOrDefault(f => f.Id == id);
    if (furnitureDto == null)
    {
      throw new ArgumentNullException("Furniture not found");
    }
    return furnitureDto.ToSingleDto(language);
  }
}