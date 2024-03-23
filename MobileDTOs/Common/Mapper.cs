namespace MobileDTOs.Common;

public static class Mapper
{
  public static CategoryDto ToDto(this Category category, Language language)
      => new()
      {
        Id = category.Id,
        Name = language switch
        {
          Language.Uz => category.NameUz,
          Language.Ru => category.NameRu,
          _ => category.NameUz
        },
        FurnituresCount = category.Furnitures.Count,
        Furnitures = category.Furnitures.Select(f => f.ToDto(language)).ToList(),
        ImageUrl = category.ImageUrl
      };

  public static SingleCategoryDto ToSingleDto(this Category category, Language language)
      => new()
      {
        Id = category.Id,
        Name = language switch
        {
          Language.Uz => category.NameUz,
          Language.Ru => category.NameRu,
          _ => category.NameUz
        },
        FurnituresCount = category.Furnitures.Count,
        ImageUrl = category.ImageUrl
      };

  public static FurnitureDto ToDto(this Furniture furniture, Language language)
  {
    FurnitureDto dto = new();
    if (furniture.Images == null || !furniture.Images.Any())
    {
      dto.FirstImage = "https://contentgrid.homedepot-static.com/hdus/en_US/DTCCOMNEW/Articles/best-furniture-for-your-home-2022-section-1.jpg";
    }
    if (furniture.Feedbacks == null || !furniture.Feedbacks.Any())
    {
      dto.AverageRate = 0;
    }

    return new()
    {
      Id = furniture.Id,
      Name = language switch
      {
        Language.Uz => furniture.NameUz,
        Language.Ru => furniture.NameRu,
        _ => furniture.NameUz
      },
      Price = furniture.Price,
      FirstImage = dto.FirstImage,
      AverageRate = dto.AverageRate
    };
  }

  public static ColorDto ColorDto(this Color color, Language language)
      => new()
      {
        Id = color.Id,
        Name = language switch
        {
          Language.Uz => color.NameUz,
          Language.Ru => color.NameRu,
          _ => color.NameUz
        },
        HexCode = color.HexCode
      };

  public static UserDto ToDto(this User user)
      => new()
      {
        FullName = user.FullName,
        AvatarUrl = user.AvatarUrl
      };

  public static FeedbackDto ToDto(this Feedback feedback)
      => new()
      {
        Id = feedback.Id,
        Rate = feedback.Rate,
        Text = feedback.Text,
        User = feedback.User.ToDto(),
        Images = feedback.Images.Select(x => x.Url).ToList(),
        CreatedAt = feedback.CreatedAt
      };

  public static SingleFurnitureDto ToSingleDto(this Furniture furniture, Language language)
  {

    SingleFurnitureDto dto = new();
    if (furniture.Images == null || !furniture.Images.Any())
    {
      dto.FirstImage = "https://contentgrid.homedepot-static.com/hdus/en_US/DTCCOMNEW/Articles/best-furniture-for-your-home-2022-section-1.jpg";
    }
    else
    {
      dto.FirstImage = furniture.Images.First().Url;
    }
    if (furniture.Feedbacks == null || !furniture.Feedbacks.Any())
    {
      dto.AverageRate = 0;
      dto.LastFeedback = null;
    }
    else
    {
      dto.AverageRate = furniture.Feedbacks.Average(f => f.Rate);
      dto.LastFeedback = furniture.Feedbacks!
                              .OrderByDescending(x => x.CreatedAt)
                              .FirstOrDefault()!
                              .ToDto();
    }

    return new()
    {
      Id = furniture.Id,
      Name = language switch
      {
        Language.Uz => furniture.NameUz,
        Language.Ru => furniture.NameRu,
        _ => furniture.NameUz
      },
      Price = furniture.Price,
      FirstImage = dto.FirstImage,
      AverageRate = dto.AverageRate,
      Description = language switch
      {
        Language.Uz => furniture.DescriptionUz,
        Language.Ru => furniture.DescriptionRu,
        _ => furniture.DescriptionUz
      },
      Quantity = furniture.Quantity,
      PreparationDays = furniture.PreparationDays,
      InQueue = furniture.InQueue,
      CategoryId = furniture.CategoryId,
      LastFeedback = dto.LastFeedback,
      CategoryName = language switch
      {
        Language.Uz => furniture.Category!.NameUz,
        Language.Ru => furniture.Category!.NameRu,
        _ => furniture.Category!.NameUz
      },
      Images = furniture.Images!.Select(x => x.Url).ToList(),
      Colors = furniture.Colors!.Select(x => x.Color!.ColorDto(language)).ToList()
    };
  }

  public static FeedbacksListDto ToDto(this List<FeedbackDto> list)
      => new()
      {
        Feedbacks = list,
        Stars = new(
              list.Count(f => f.Rate == 1),
              list.Count(f => f.Rate == 2),
              list.Count(f => f.Rate == 3),
              list.Count(f => f.Rate == 4),
              list.Count(f => f.Rate == 5),
              list.Count,
              list.Count == 0 ? 0 : list.Average(f => f.Rate))
      };

  public static Language ToLanguage(this string lang)
      => lang.ToLower() switch
      {
        "uz" => Language.Uz,
        "ru" => Language.Ru,
        _ => Language.Uz
      };
}