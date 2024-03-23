namespace MobileBLL.Services;

public class MobileService(IUnitOfWork unitOfWork,
                           IDistributedCache cache)
    : IMobileService
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;
  private readonly IDistributedCache _cache = cache;

  public ICategoryService Categories
      => new CategoryService(_unitOfWork, _cache);

  public IFurnitureService Furnitures
      => new FurnitureService(_unitOfWork, _cache);

  public IFeedbackService Feedbacks
      => new FeedbackService(_unitOfWork, _cache);

  public void Dispose()
     => GC.SuppressFinalize(this);
}
