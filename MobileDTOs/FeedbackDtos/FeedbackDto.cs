namespace MobileDTOs.FeedbackDtos;

public class FeedbackDto : BaseDto
{
  public int Rate { get; set; }
  public string Text { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public UserDto User { get; set; } = new();
  public List<string> Images { get; set; } = new();
}
