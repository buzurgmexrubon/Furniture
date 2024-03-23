namespace MobileBLL.Interfaces;

public interface IFeedbackService
{
  Task<FeedbacksListDto> GetFeedbacksListAsync(int furnitureId);
  Task<FeedbackDto> GetFeedbackAsync(int id);
  Task<FeedbackDto> CreateFeedbackAsync(AddFeedbackDto feedbackDto);
  Task BanFeedbackAsync(int id, string userId);
}