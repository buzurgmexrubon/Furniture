namespace MobileDTOs.CategoryDtos;

public class CategoryDto : SingleCategoryDto
{
  public List<FurnitureDto> Furnitures { get; set; } = new();
}