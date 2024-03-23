namespace MobileDTOs.CategoryDtos;

public class SingleCategoryDto : BaseDto
{
  public string Name { get; set; } = string.Empty;
  public string ImageUrl { get; set; } = string.Empty;
  public int FurnituresCount { get; set; }
}