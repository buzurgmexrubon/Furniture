namespace BLL.Common;

public static partial class Validator
{
  public static bool IsValidCategory(this Category category)
      => !string.IsNullOrWhiteSpace(category.NameUz)
      && !string.IsNullOrWhiteSpace(category.NameRu);

  public static bool IsExist(this Category category,
                                IEnumerable<Category> categories)
      => categories.Any(c => c.NameUz == category.NameUz
                          && c.NameRu == category.NameRu);

  public static bool IsNotUnique(this Category category,
                                               IEnumerable<Category> categories)
      => categories.Any(c => c.NameUz == category.NameUz
                          && c.NameRu == category.NameRu
                          && c.Id != category.Id);
}