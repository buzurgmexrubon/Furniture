namespace MobileBLL.Interfaces;

public interface IMobileService : IDisposable
{
  ICategoryService Categories { get; }
  IFurnitureService Furnitures { get; }
  IFeedbackService Feedbacks { get; }
}