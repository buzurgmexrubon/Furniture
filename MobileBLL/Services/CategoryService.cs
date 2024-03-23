namespace MobileBLL.Services;

public class CategoryService(IUnitOfWork unitOfWork,
                              IDistributedCache cache)
    : ICategoryService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly ICacheService<Category> _cacheService =
      new CacheService<Category>(cache);
  private const string _cacheKey = "categories";

  public async Task<List<SingleCategoryDto>> GetCategoriesAsync(Language language)
  {
    var categories = await _cacheService.GetFromCacheAsync(_cacheKey);
    if (categories == null)
    {
      categories = await _unitOfWork.Categories.GetAllWithDependenciesAsync();
      await _cacheService.SaveToCacheAsync(JsonConvert.SerializeObject(categories, Formatting.None,
                                         new JsonSerializerSettings()
                                         {
                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                         }), _cacheKey);
    }

    var list = categories.Select(c => c.ToSingleDto(language)).ToList();
    return list;
  }

  public async Task<CategoryDto> GetSingleCategoryAsync(int id, Language language)
  {
    var categories = await _cacheService.GetFromCacheAsync(_cacheKey);
    if (categories == null)
    {
      categories = await _unitOfWork.Categories.GetAllWithDependenciesAsync();

      if (categories != null && categories.Any())
      {
        await _cacheService.SaveToCacheAsync(JsonConvert.SerializeObject(categories, Formatting.None,
                                       new JsonSerializerSettings()
                                       {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                       }), _cacheKey);
      }
    }

    var category = categories!.FirstOrDefault(c => c.Id == id);
    if (category == null)
    {
      throw new ArgumentNullException("Category not found");
    }
    return category.ToDto(language);
  }
}