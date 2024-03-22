namespace DTOs.FurnitureDtos;

public class SingleFurnitureDto
{
  public string NameUz { get; set; } = string.Empty;

  public string NameRu { get; set; } = string.Empty;

  public string DescriptionUz { get; set; } = string.Empty;

  public string DescriptionRu { get; set; } = string.Empty;

  public int Quantity { get; set; }

  public int PreparationDays { get; set; }

  public int InQueue { get; set; }

  public decimal Price { get; set; }

  public SingleCategoryDto? Category { get; set; } = new();

  public List<string>? Images { get; set; }

  public List<SingleColorDto>? Colors { get; set; }

  public bool IsActive { get; set; }

  public static implicit operator SingleFurnitureDto(Furniture furniture)
      => new()
      {
        NameUz = furniture.NameUz,
        NameRu = furniture.NameRu,
        DescriptionUz = furniture.DescriptionUz,
        DescriptionRu = furniture.DescriptionRu,
        Quantity = furniture.Quantity,
        PreparationDays = furniture.PreparationDays,
        InQueue = furniture.InQueue,
        Price = furniture.Price,
        Category = (SingleCategoryDto)furniture.Category!,
        Images = furniture.Images.Select(x => x.Url).ToList(),
        Colors = furniture.Colors!.Select(x => (SingleColorDto)x.Color!)
                                    .ToList(),
        IsActive = furniture.IsActive
      };
}