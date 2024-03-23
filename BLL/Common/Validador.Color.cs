namespace BLL.Common;

public static partial class Validator
{
  public static bool IsValidColor(this Color color)
      => !string.IsNullOrWhiteSpace(color.NameUz)
      && !string.IsNullOrWhiteSpace(color.NameUz);

  public static bool IsExist(this Color color,
                                IEnumerable<Color> colors)
      => colors.Any(c => c.NameUz == color.NameUz
                      && c.NameRu == color.NameRu);

  public static bool IsNotUnique(this Color color,
                                        IEnumerable<Color> colors)
      => colors.Any(c => c.NameUz == color.NameUz
                      && c.NameRu == color.NameRu
                      && c.Id != color.Id);
}