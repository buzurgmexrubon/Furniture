namespace MobileDTOs.FurnitureDtos;

public class FurnitureDto : BaseDto
{
  public string Name { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public string FirstImage { get; set; } = string.Empty;
  public double AverageRate { get; set; }
}