namespace MobileDTOs.FurnitureDtos;

public class SingleFurnitureDto : FurnitureDto
{
  public string Description { get; set; } = string.Empty;
  public int Quantity { get; set; }
  public int PreparationDays { get; set; }
  public int InQueue { get; set; }
  public List<string> Images { get; set; } = new();
  public List<ColorDto> Colors { get; set; } = new();
  public int CategoryId { get; set; }
  public string CategoryName { get; set; } = string.Empty;
  public FeedbackDto LastFeedback { get; set; } = new();
}