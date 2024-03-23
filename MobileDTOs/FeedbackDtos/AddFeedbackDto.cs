namespace MobileDTOs.FeedbackDtos;

public class AddFeedbackDto
{
  public int Rate { get; set; }
  public string Text { get; set; } = string.Empty;
  public int FurnitureId { get; set; }
  public string UserId { get; set; } = string.Empty;
  public List<string> Images { get; set; } = new();

  public static implicit operator Feedback(AddFeedbackDto dto)
      => new()
      {
        Rate = dto.Rate,
        Text = dto.Text,
        FurnitureId = dto.FurnitureId,
        UserId = dto.UserId
      };
}