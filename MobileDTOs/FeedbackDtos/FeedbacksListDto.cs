namespace MobileDTOs.FeedbackDtos;

public class FeedbacksListDto
{
  public Stars Stars { get; set; } = new(0, 0, 0, 0, 0, 0, 0);
  public List<FeedbackDto> Feedbacks { get; set; } = new();
}

public record Stars(int star1Count, int star2Count, int star3Count, int star4Count, int star5Count, int totalCount, double averageRating);