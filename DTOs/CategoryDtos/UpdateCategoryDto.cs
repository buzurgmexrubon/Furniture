namespace DTOs.CategoryDtos;

public class UpdateCategoryDto : AddCategoryDto
{
  public int Id { get; set; }

  public static implicit operator Category(UpdateCategoryDto updateCategoryDto)
      => new()
      {
        Id = updateCategoryDto.Id,
        NameUz = updateCategoryDto.NameUz,
        NameRu = updateCategoryDto.NameRu,
        ImageUrl = updateCategoryDto.ImageUrl,
        UpdatedAt = LocalTime.GetUtc5Time()
      };
}