namespace DTOs.CategoryDtos;

public class SingleCategoryDto : BaseDto
{
  public string NameUz { get; set; } = null!;

  public string NameRu { get; set; } = null!;

  public string ImageUrl { get; set; } = null!;

  public bool IsActive { get; set; }

  public static implicit operator SingleCategoryDto(Category category)
      => new()
      {
        Id = category.Id,
        NameUz = category.NameUz,
        NameRu = category.NameRu,
        ImageUrl = category.ImageUrl,
        IsActive = category.IsActive
      };
}