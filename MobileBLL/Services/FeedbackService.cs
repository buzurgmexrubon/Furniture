namespace MobileBLL.Services;

public class FeedbackService(IUnitOfWork unitOfWork,
                             IDistributedCache cache)
    : IFeedbackService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly ICacheService<Feedback> _cacheService =
      new CacheService<Feedback>(cache);
  private readonly string _cacheKey = "feedbacks";
  private readonly string _cacheKeyFurniture = "furnitures";
  private readonly string _cacheKeyCategory = "categories";

  public async Task BanFeedbackAsync(int id, string userId)
  {
    var ban = new FeedbackBan()
    {
      FeedbackId = id,
      UserId = userId
    };
    _unitOfWork.FeedbackBans.Add(ban);
    await _unitOfWork.SaveAsync();
  }

  public async Task<FeedbackDto> CreateFeedbackAsync(AddFeedbackDto feedbackDto)
  {
    var feedback = (Feedback)feedbackDto;
    var result = _unitOfWork.Feedbacks.Add(feedback);
    await _unitOfWork.SaveAsync();

    foreach (var imageUrl in feedbackDto.Images)
    {
      var image = new Image
      {
        Url = imageUrl,
        FeedbackId = result.Id,
        FurnitureId = null,
        Furniture = null,
        Feedback = null
      };
      _unitOfWork.Images.Add(image);
      await _unitOfWork.SaveAsync();
    }

    await _cacheService.RemoveFromCacheAsync(_cacheKey);
    await _cacheService.RemoveFromCacheAsync(_cacheKeyFurniture);
    await _cacheService.RemoveFromCacheAsync(_cacheKeyCategory);
    feedback = await _unitOfWork.Feedbacks.GetAsyncWithDependencies(result.Id);
    return feedback!.ToDto();
  }

  public async Task<FeedbackDto> GetFeedbackAsync(int id)
  {
    var feedback = await _unitOfWork.Feedbacks.GetAsyncWithDependencies(id);
    if (feedback == null)
    {
      throw new ArgumentNullException("Feedback not found");
    }
    return feedback.ToDto();
  }

  public async Task<FeedbacksListDto> GetFeedbacksListAsync(int furnitureId)
  {
    var furniture = await _unitOfWork.Furnitures.GetByIdAsyncWithDependencies(furnitureId);
    if (furniture == null)
    {
      throw new ArgumentNullException("Furniture not found");
    }

    IEnumerable<Feedback> feedbacks = null;// await _cacheService.GetFromCacheAsync(_cacheKey);
    if (feedbacks == null)
    {
      feedbacks = await _unitOfWork.Feedbacks.GetAllAsyncWithDependencies();
      await _cacheService.SaveToCacheAsync(JsonConvert.SerializeObject(feedbacks, Newtonsoft.Json.Formatting.None,
                                          new JsonSerializerSettings()
                                          {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                          }), _cacheKey);
    }

    var list = feedbacks.Where(f => f.FurnitureId == furnitureId)
                        .Select(f => f.ToDto())
                        .ToList();
    return list.ToDto();
  }
}