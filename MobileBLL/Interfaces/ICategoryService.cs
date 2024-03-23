namespace MobileBLL.Interfaces;

public interface ICategoryService
{
  Task<List<SingleCategoryDto>> GetCategoriesAsync(Language language);

  Task<CategoryDto> GetSingleCategoryAsync(int id, Language language);
}