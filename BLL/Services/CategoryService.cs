namespace BLL.Services;

public class CategoryService(IUnitOfWork unitOfWork,
                             IDistributedCache cache)
    : ICategoryService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly IDistributedCache _cache = cache;
  private const string _cacheKey = "categories";

  /// <summary>
  /// Create new category
  /// </summary>
  /// <param name="categoryDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto,
                                             Language language)
  {
    if (categoryDto is null)
    {
      throw new FurnitureException("Category was null");
    }

    var model = (Category)categoryDto;
    if (!model.IsValidCategory())
    {
      throw new FurnitureException("Category is not valid");
    }

    var categories = await _unitOfWork.Categories.GetAllAsync();
    if (model.IsExist(categories))
    {
      throw new FurnitureException("Category already exists");
    }

    model = _unitOfWork.Categories.Add(model);
    await _unitOfWork.SaveAsync();
    await _cache.RemoveAsync(_cacheKey);
    return model.ToDto(language);
  }

  /// <summary>
  /// Action with category (archive, unarchive, delete)
  /// </summary>
  /// <param name="id"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task ActionAsync(int id, ActionType action)
  {
    var category = await _unitOfWork.Categories.GetByIdAsync(id);
    if (category is null)
    {
      throw new FurnitureException("Category not found");
    }

    switch (action)
    {
      case ActionType.Archive:
        {
          category.IsActive = false;
          _unitOfWork.Categories.Update(category);
        }
        break;
      case ActionType.UnArchive:
        {
          category.IsActive = true;
          _unitOfWork.Categories.Update(category);
        }
        break;
      case ActionType.Delete:
        {
          _unitOfWork.Categories.Delete(id);
        }
        break;
    }
    category.UpdatedAt = LocalTime.GetUtc5Time();
    await _unitOfWork.SaveAsync();
    await _cache.RemoveAsync(_cacheKey);
  }

  /// <summary>
  /// Get all categories with pagination
  /// </summary>
  /// <param name="pageSize"></param>
  /// <param name="pageNumber"></param>
  /// <returns></returns>
  public async Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber, Language language)
  {
    var categories = await _unitOfWork.Categories.GetAllAsync();
    var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
    return new PagedList<CategoryDto>(categoryDtos, categoryDtos.Count, pageNumber, pageSize);
  }

  /// <summary>
  /// Get all categories
  /// </summary>
  /// <returns></returns>
  public async Task<List<CategoryDto>> GetAllAsync(Language language)
  {
    var categories = await _unitOfWork.Categories.GetAllAsync();
    var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
    return categoryDtos;
  }

  /// <summary>
  /// Get category by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<CategoryDto> GetByIdAsync(int id, Language language)
  {
    var category = await _unitOfWork.Categories.GetByIdAsync(id);
    return category is null ?
        throw new FurnitureException("Category not found") : category.ToDto(language);
  }

  /// <summary>
  /// Update category
  /// </summary>
  /// <param name="categoryDto"></param>
  /// <returns></returns>
  /// <exception cref="FurnitureException"></exception>
  public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto,
                                             Language language)
  {
    if (categoryDto is null)
    {
      throw new FurnitureException("Category was null");
    }

    var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
    if (category is null)
    {
      throw new FurnitureException("Category not found");
    }

    var model = (Category)categoryDto;
    if (!model.IsValidCategory())
    {
      throw new FurnitureException("Category is not valid");
    }

    var categories = await _unitOfWork.Categories.GetAllAsync();
    if (model.IsNotUnique(categories))
    {
      throw new FurnitureException("Category already exists");
    }

    _unitOfWork.Categories.Update(model);
    await _unitOfWork.SaveAsync();
    model = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
    if (model is null)
    {
      throw new FurnitureException("Category not found");
    }
    await _cache.RemoveAsync(_cacheKey);
    return model.ToDto(language);
  }

  /// <summary>
  /// Get category by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public async Task<SingleCategoryDto> GetByIdAsync(int id)
  {
    var category = await _unitOfWork.Categories.GetByIdAsync(id);
    if (category is null)
    {
      throw new ArgumentNullException("Category not found");
    }

    return (SingleCategoryDto)category;
  }
}